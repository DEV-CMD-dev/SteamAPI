using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Achievement;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace BusinessLogic.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public AchievementService(
           SteamDbContext context,
           IMapper mapper,
           IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }
        public async Task<PaginatedList<AchievementDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Achievements
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ProjectTo<AchievementDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<AchievementDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<AchievementDto> GetById(int id)
        {
            var achievement = await _context.Achievements.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (achievement == null)
                throw new HttpException($"Achievement with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<AchievementDto>(achievement);
        }

        public async Task Create(string userId, CreateAchievementDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new HttpException("Achievements name can not be empty", HttpStatusCode.BadRequest);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(dto.GameId);

            var newAchievement = _mapper.Map<Achievement>(dto);
            _context.Achievements.Add(newAchievement);
            await _context.SaveChangesAsync();
        }

        public async Task Patch(int id, string userId, PatchAchievementDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) && string.IsNullOrWhiteSpace(dto.IconUrl))
                throw new HttpException("Achievement name and picture can not be empty", HttpStatusCode.BadRequest);

            await Update(userId, id, dto);
        }

        public async Task Put(int id, string userId, PutAchievementDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrEmpty(dto.IconUrl))
                throw new HttpException("Achievement name or picture can not be empty", HttpStatusCode.BadRequest);

            await Update(userId, id, dto);
        }

        public async Task Delete(string userId, int id)
        {
            var achievement = await _context.Achievements.FindAsync(id);

            if (achievement == null)
                throw new HttpException($"Achievement with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(achievement.GameId);

            _context.Achievements.Remove(achievement);
            await _context.SaveChangesAsync();
        }

        private async Task Update<TDto>(string userId, int id, TDto dto)
        {
            var existingAchievement = await _context.Achievements.FindAsync(id);

            if (existingAchievement == null)
                throw new HttpException($"Achievement with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(existingAchievement.GameId);

            _mapper.Map(dto, existingAchievement);

            await _context.SaveChangesAsync();
        }
    }
}
