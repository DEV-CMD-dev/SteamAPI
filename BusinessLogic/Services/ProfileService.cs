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

            if (profile == null)
                throw new HttpException($"Profile with UserId {userId} not found", HttpStatusCode.NotFound);

            var result = _mapper.Map<ProfileDto>(profile);
            result.RecentlyPlayedGames = await _context.UserGames
                .AsNoTracking()
                .Where(userGame => userGame.UserId == userId && userGame.Game != null)
                .OrderByDescending(userGame => userGame.LastPlayDate)
                .Take(10)
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

            // Only map non-null properties
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
    }
}
