using AutoMapper;
using BusinessLogic.Classes;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Services
{
    public class GameService : IGameService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;

        public GameService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameDto>> GetAll()
        {
            var games = await _context.Games.AsNoTracking().ToListAsync();
            return _mapper.Map<IEnumerable<GameDto>>(games);
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

            dto.ReleaseDate ??= DateTime.UtcNow;

            var newGame = _mapper.Map<Game>(dto);
            newGame.DeveloperId = developerId;

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            return _mapper.Map<GameDto>(newGame);
        }

        public async Task Update(int id, UpdateGameDto dto)
        {
            var existingGame = await _context.Games.FindAsync(id);
            if (existingGame == null)
                throw new HttpException($"Game with Id {id} not found", HttpStatusCode.NotFound);

            _mapper.Map(dto, existingGame);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int gameId, string userId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null)
                throw new HttpException($"Game with ID {gameId} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new HttpException($"User with ID {userId} not found", HttpStatusCode.NotFound);

            bool isModerator = user.UserRole == UserRole.Moderator;
            bool isOwner = game.DeveloperId == userId;

            if (isModerator || isOwner)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new HttpException("You do not have permission to delete this game", HttpStatusCode.Forbidden);
            }
        }
    }
}