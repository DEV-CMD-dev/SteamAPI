using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using DataAccess.Enums;
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

        public async Task<PaginatedList<GameDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Games
                .AsNoTracking()
                .OrderBy(g => g.Id)
                .ProjectTo<GameDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<GameDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<GameDto> GetById(int id)
        {
            var game = await _context.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (game == null)
                throw new HttpException($"Game with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> Create(string developerId, CreateGameDto dto)
        {
            var developer = await _context.Users.FindAsync(developerId);

            if (developer == null)
                throw new HttpException($"User with ID {developerId} not found", HttpStatusCode.NotFound);

            if (developer.UserRole != UserRole.Developer)
                throw new HttpException("Only developers can create games", HttpStatusCode.Forbidden);

            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Description))
                throw new HttpException("Title and description can not be empty", HttpStatusCode.BadRequest);

            dto.ReleaseDate ??= DateTime.UtcNow;

            var newGame = _mapper.Map<Game>(dto);
            newGame.DeveloperId = developerId;

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return _mapper.Map<GameDto>(newGame);
        }

        public async Task Patch(int id, string userId, PatchGameDto dto)
        {
            await Update(id, userId, dto);
        }

        public async Task Put(int id, string userId, PutGameDto dto)
        {
            await Update(id, userId, dto);
        }

        public async Task Delete(int gameId, string userId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
                throw new HttpException($"Game with ID {gameId} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new HttpException($"User with ID {userId} not found", HttpStatusCode.NotFound);

            if (user.CanManageGame(gameId))
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new HttpException("You do not have permission to delete this game", HttpStatusCode.Forbidden);
            }
        }

        private async Task Update<TDto>(int id, string userId, TDto dto)
        {
            var existingGame = await _context.Games.FindAsync(id);

            if (existingGame == null)
                throw new HttpException($"Game with Id {id} not found", HttpStatusCode.NotFound);
            _mapper.Map(dto, existingGame);

            await _context.SaveChangesAsync();
        }
    }
}