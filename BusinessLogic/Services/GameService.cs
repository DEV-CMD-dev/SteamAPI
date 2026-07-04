using AutoMapper;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using DataAccess.Enums;

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

        public async Task<IList<GameDto>> GetAll()
        {
            var games = await _context.Games.ToListAsync();
            var gameDtos = _mapper.Map<IList<GameDto>>(games);

            return gameDtos;
        }
        public async Task<GameDto?> Get(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null) 
                throw new Exception($"Game with ID {id} not found.");

            var gameDto = _mapper.Map<GameDto>(game);

            return gameDto;
        }
        public async Task<GameDto> Create(CreateGameDto dto)
        {
            var develeoper = await _context.Users.AnyAsync(d => d.Id == dto.DeveloperId);

            if (!develeoper)
                throw new Exception($"Developer with ID {dto.DeveloperId} not found.");
            if (dto.ReleaseDate.HasValue && dto.ReleaseDate.Value < DateTime.UtcNow)
                throw new Exception("Release date cannot be in the past.");
            if (dto.Price < 0)
                throw new Exception("Price cannot be negative.");

            dto.ReleaseDate ??= DateTime.UtcNow;

            var newGame = _mapper.Map<Game>(dto);

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();

            var gameDto = _mapper.Map<GameDto>(newGame);

            return gameDto;
        }
        public async Task Update(UpdateGameDto dto)
        {
            var existingGame = await _context.Games.FindAsync(dto.Id);

            if (existingGame == null)
                throw new Exception($"Game with Id {dto.Id} not found.");

            _mapper.Map(dto, existingGame);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(DeleteGameDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            var game = await _context.Games.FindAsync(dto.GameId);

            if (game == null)
                throw new Exception($"Game with ID {dto.GameId} not found.");

            if (user.UserRole == UserRole.Developer || user.UserRole == UserRole.Moderator)
            {
                if (game.DeveloperId == dto.UserId)
                {
                    _context.Games.Remove(game);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("You do not have permission to delete this game.");
                }
            }
            else
            {
                throw new Exception("You do not have permission to delete this game.");
            }
        }
    }
}
