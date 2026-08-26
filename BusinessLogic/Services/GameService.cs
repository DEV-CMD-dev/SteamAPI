using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Extensions;
using BusinessLogic.Extensions.SearchFilters;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public GameService(
            SteamDbContext context,
            IMapper mapper,
            IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task<PaginatedList<GameDto>> GetAll(int pageNumber, int pageSize, GameParameters gameParams, bool withScreenshots = false)
        {
            var query = _context.Games
             .ApplyFilters(gameParams)
             .OrderBy(g => g.Title);

            IQueryable<GameDto> games = withScreenshots
                ? query.ProjectTo<GameDto>(_mapper.ConfigurationProvider, dest => dest.Screenshots)
                : query.ProjectTo<GameDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<GameDto>.CreateAsync(games, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<PaginatedList<GameDto>> GetUserLibrary(string userId, int pageNumber, int pageSize, GameParameters gameParams, bool withScreenshots = false)
        {
            var query = _context.Games
                .Where(g => g.UserGames.Any(ug => ug.UserId == userId)) 
                .ApplyFilters(gameParams)
                .OrderBy(g => g.Title);

            IQueryable<GameDto> games = withScreenshots
                ? query.ProjectTo<GameDto>(_mapper.ConfigurationProvider, dest => dest.Screenshots)
                : query.ProjectTo<GameDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<GameDto>.CreateAsync(games, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<GameDto> GetById(int id)
        {
            var game = await _context.Games
                .Include(g => g.Tags)
                .Include(g => g.Screenshots)
                .Include(g => g.Developer)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (game == null)
                throw new HttpException($"Game with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> Create(string userId, CreateGameDto dto)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId).EnsureDeveloper();

            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Description))
                throw new HttpException("Title and description can not be empty", HttpStatusCode.BadRequest);

            dto.ReleaseDate ??= DateTime.UtcNow;

            var newGame = _mapper.Map<Game>(dto);
            newGame.DeveloperId = userId;

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return _mapper.Map<GameDto>(newGame);
        }

        public async Task Patch(int id, string userId, PatchGameDto dto)
        {
            var game = await GetGameForUpdate(id, userId);

            if (dto.Title != null)
                game.Title = dto.Title;

            if (dto.Description != null)
                game.Description = dto.Description;

            if (dto.ReleaseDate.HasValue)
                game.ReleaseDate = dto.ReleaseDate.Value;

            if (dto.Price.HasValue)
                game.Price = dto.Price.Value;

            if (dto.Discount.HasValue)
                game.Discount = dto.Discount.Value;

            if (dto.SystemRequirements != null)
                game.SystemRequirements = dto.SystemRequirements;

            if (dto.CoverImageHorizontal != null)
                game.CoverImageHorizontal = dto.CoverImageHorizontal;

            if (dto.CoverImageVertical != null)
                game.CoverImageVertical = dto.CoverImageVertical;

            if (dto.TagIds != null)
                await game.SetTagsAsync(_context, dto.TagIds);

            await _context.SaveChangesAsync();
        }

        public async Task Put(int id, string userId, PutGameDto dto)
        {
            var game = await GetGameForUpdate(id, userId);

            _mapper.Map(dto, game);

            if (dto.TagIds != null)
                await game.SetTagsAsync(_context, dto.TagIds);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int gameId, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId);
            
            var hasAccess = await _context.Users
                .Where(u => u.Id == userId)
                .AnyAsync(u => u.DevelopedGames.Any(g => g.Id == gameId));

            if (!hasAccess)
                throw new HttpException("You do not have access to this game", HttpStatusCode.Forbidden);
            
            var rowsAffected = await _context.Games
                .Where(g => g.Id == gameId)
                .ExecuteDeleteAsync();

            if (rowsAffected == 0)
                throw new HttpException($"Game with ID {gameId} not found", HttpStatusCode.NotFound);
        }

        private async Task<Game> GetGameForUpdate(int id, string userId)
        {
            var game = await _context.Games
                .Include(g => g.Tags)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (game == null)
                throw new HttpException($"Game with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);

            user.EnsureExists(userId).EnsureHasAccessToGame(id);

            return game;
        }
    }
}