using System.Net;
using AutoMapper;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ProfileService : IProfileService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;

        public ProfileService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProfileDto> GetById(string userId)
        {
            var profile = await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);
            var userBio = await _context.Users.AsNoTracking()
                .Where(u => userId == u.Id)
                .Select(u => u.Bio)
                .FirstOrDefaultAsync();

            var userName = await _context.Users.AsNoTracking()
               .Where(u => userId == u.Id)
               .Select(u => u.UserName)
               .FirstOrDefaultAsync();

            if (profile == null)
                throw new HttpException($"Profile with UserId {userId} not found", HttpStatusCode.NotFound);

            var result = _mapper.Map<ProfileDto>(profile);
            result.Bio = userBio;
            result.UserName = userName;
            result.RecentlyPlayedGames = await _context.UserGames
                .AsNoTracking()
                .Where(userGame => userGame.UserId == userId && userGame.Game != null)
                .OrderByDescending(userGame => userGame.LastPlayDate)
                .Take(3)
                .Select(userGame => new RecentGameDto
                {
                    Id = userGame.GameId,
                    Title = userGame.Game!.Title,
                    CoverImageHorizontal = userGame.Game.CoverImageHorizontal,
                    LastPlayDate = userGame.LastPlayDate,
                    PlayTimeMinutes = userGame.PlayTimeMinutes,
                    Achievements = userGame.Game.Achievements!
                        .Select(achievement => new AchievementProgressDto
                        {
                            Id = achievement.Id,
                            Name = achievement.Name,
                            IconUrl = achievement.IconUrl,
                            IsUnlocked = _context.UserAchievements.Any(userAchievement =>
                                userAchievement.UserId == userId && userAchievement.AchievementId == achievement.Id)
                        })
                        .ToList()
                })
                .ToListAsync();

            return result;
        }


        public async Task<MiniProfileDto> GetMyProfile(string userId)
        {
            var profile = await _context.Profiles
                .Where(p => p.UserId == userId) 
                .Select(p => new MiniProfileDto
                {
                    Avatar = p.Avatar,
                    Name = p.User.UserName,     
                    UserId = p.UserId
                })
                .FirstOrDefaultAsync();

            if (profile == null)
                throw new HttpException($"Profile with UserId {userId} not found", HttpStatusCode.NotFound);

            return profile;
        }


        public async Task Patch(string userId, PatchProfileDto dto)
        {
            await Update(userId, dto);
        }

        public async Task Put(string userId, PutProfileDto dto)
        {
            await Update(userId, dto);
        }

        private async Task Update<TDto>(string userId, TDto dto)
        {
            var existingProfile = await _context.Profiles.FindAsync(userId);

            if (existingProfile == null)
                throw new HttpException($"Profile with UserId {userId} not found", HttpStatusCode.NotFound);

            if (dto is PatchProfileDto patchDto)
            {
                if (patchDto.Avatar != null)
                    existingProfile.Avatar = patchDto.Avatar;
                if (patchDto.Badges != null)
                    existingProfile.Badges = patchDto.Badges;
                if (patchDto.Showcase != null)
                    existingProfile.Showcase = patchDto.Showcase;
            }
            else
            {
                _mapper.Map(dto, existingProfile);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<List<ProfileSearchResultDto>> SearchByUserName(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<ProfileSearchResultDto>();

            return await _context.Profiles
                .AsNoTracking()
                .Where(p => p.User != null && EF.Functions.Like(p.User.UserName, $"%{query}%"))
                .OrderBy(p => p.User!.UserName)
                .Take(10)
                .Select(p => new ProfileSearchResultDto
                {
                    UserId = p.UserId,
                    UserName = p.User!.UserName,
                    Avatar = p.Avatar
                })
                .ToListAsync();
        }
    }
}
