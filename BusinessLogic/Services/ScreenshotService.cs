using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Screenshot;
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
    public class ScreenshotService : IScreenshotService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public ScreenshotService(
           SteamDbContext context,
           IMapper mapper,
           IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }
        public async Task<PaginatedList<ScreenshotDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Screenshots
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ProjectTo<ScreenshotDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<ScreenshotDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<ScreenshotDto> GetById(int id)
        {
            var screenshot = await _context.Screenshots.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (screenshot == null)
                throw new HttpException($"Screenshot with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<ScreenshotDto>(screenshot);
        }

        public async Task Create(string userId, CreateScreenshotDto dto)
        {
            if(await _context.Games.FindAsync(dto.GameId) == null)
                throw new HttpException($"Game with ID {dto.GameId} not found", HttpStatusCode.NotFound);

            if (string.IsNullOrWhiteSpace(dto.Url))
                throw new HttpException("Screenshot URL can not be empty", HttpStatusCode.BadRequest);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(dto.GameId);

            var newScreenshot = _mapper.Map<Screenshot>(dto);
            _context.Screenshots.Add(newScreenshot);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, string userId, UpdateScreenshotDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Url))
                throw new HttpException("Screenshot URL can not be empty", HttpStatusCode.BadRequest);

            var screenshot = await GetScreenshotForUpdate(id, userId);

            _mapper.Map(dto, screenshot);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var screenshot = await _context.Screenshots.FindAsync(id);

            if (screenshot == null)
                throw new HttpException($"Screenshot with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(screenshot.GameId);

            _context.Screenshots.Remove(screenshot);
            await _context.SaveChangesAsync();
        }

        private async Task<Screenshot> GetScreenshotForUpdate(int id, string userId)
        {
            var screenshot = await _context.Screenshots
                .FirstOrDefaultAsync(s => s.Id == id);
            if (screenshot == null)
                throw new HttpException($"Screenshot with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);

            user.EnsureExists(userId).EnsureHasAccessToGame(screenshot.GameId);

            return screenshot;
        }

    }
}
