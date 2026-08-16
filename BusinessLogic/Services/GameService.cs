using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Extensions;
using BusinessLogic.Extensions.SearchFilters;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Interfaces.BlobStorage;
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
        private readonly IGameBlobStorageService _gameBlobStorageService;

        public GameService(
            SteamDbContext context,
            IMapper mapper,
            IOptions<FrontendOptions> frontendOptions,
            IGameBlobStorageService gameBlobStorageService)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
            _gameBlobStorageService = gameBlobStorageService;
        }

        public async Task<PaginatedList<GameDto>> GetAll(int pageNumber, int pageSize, GameParameters gameParams)
        {
            var query = _context.Games
                .ApplyFilters(gameParams);

            var games = query
                .AsNoTracking()
                .OrderBy(g => g.Title)
                .ProjectTo<GameDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<GameDto>.CreateAsync(games, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<GameDto> GetById(int id)
        {
            var game = await _context.Games
                .Include(g => g.Tags)
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

            if (dto.CoverImageHorizontal != null)
                newGame.CoverImageHorizontal = await _gameBlobStorageService.UploadCoverAsync(dto.CoverImageHorizontal);

            if (dto.CoverImageVertical != null)
                newGame.CoverImageVertical = await _gameBlobStorageService.UploadCoverAsync(dto.CoverImageVertical);

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
                game.CoverImageHorizontal = await _gameBlobStorageService.ReplaceCoverAsync(dto.CoverImageHorizontal, game.CoverImageHorizontal);

            if (dto.CoverImageVertical != null)
                game.CoverImageVertical = await _gameBlobStorageService.ReplaceCoverAsync(dto.CoverImageVertical, game.CoverImageVertical);

            if (dto.TagIds != null)
                await game.SetTagsAsync(_context, dto.TagIds);

            await _context.SaveChangesAsync();
        }

        public async Task Put(int id, string userId, PutGameDto dto)
        {
            var game = await GetGameForUpdate(id, userId);

            var oldHorizontal = game.CoverImageHorizontal;
            var oldVertical = game.CoverImageVertical;

            _mapper.Map(dto, game);

            if (dto.CoverImageHorizontal != null)
                game.CoverImageHorizontal = await _gameBlobStorageService.ReplaceCoverAsync(dto.CoverImageHorizontal, oldHorizontal);

            if (dto.CoverImageVertical != null)
                game.CoverImageVertical = await _gameBlobStorageService.ReplaceCoverAsync(dto.CoverImageVertical, oldVertical);

            if (dto.TagIds != null)
                await game.SetTagsAsync(_context, dto.TagIds);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int gameId, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId);

            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
                throw new HttpException("Game not found", HttpStatusCode.NotFound);

            var hasAccess = await _context.Users
                .Where(u => u.Id == userId)
                .AnyAsync(u => u.DevelopedGames.Any(g => g.Id == gameId));

            if (!hasAccess)
                throw new HttpException("You do not have access to this game", HttpStatusCode.Forbidden);

            if (game.CoverImageHorizontal != null)
                await _gameBlobStorageService.DeleteCoverAsync(game.CoverImageHorizontal);

            if (game.CoverImageVertical != null)
                await _gameBlobStorageService.DeleteCoverAsync(game.CoverImageVertical);

            await _context.Games
                .Where(g => g.Id == gameId)
                .ExecuteDeleteAsync();
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