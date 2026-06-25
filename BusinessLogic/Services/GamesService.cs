using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Interfaces;
using BusinessLogic.DTOs.Game;
using DataAccess.Data.Entities;

namespace BusinessLogic.Services
{
    public class GamesService : IGamesService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        public GamesService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IList<GameDto>> GetAll()
        {
            var gamesFromDb = await _context.Game.ToListAsync();
            var gameDtos = _mapper.Map<IList<GameDto>>(gamesFromDb);
            return gameDtos;
        }
        public async Task<GameDto?> Get(int id)
        {
            var gameFromDb = await _context.Game.FindAsync(id);
            if (gameFromDb == null)
            {
                return null;
            }
            var gameDto = _mapper.Map<GameDto>(gameFromDb);
            return gameDto;
        }
        public async Task<GameDto> Create(CreateGameDto model,string url)
        {
            var gameEntity = _mapper.Map<Game>(model);
            gameEntity.CoverImage = url;
            _context.Game.Add(gameEntity);
            await _context.SaveChangesAsync();
            var gameDto = _mapper.Map<GameDto>(gameEntity);
            return gameDto;
        }
        public async Task Update(EditGameDto model, string url)
        {
            var existingGame = await _context.Game.FindAsync(model.Id);
            if (existingGame == null)
                throw new KeyNotFoundException($"Гра з Id {model.Id} не знайдена.");
            var mappedgame = _mapper.Map<GameDto>(model);
            mappedgame.CoverImage = url;
            _mapper.Map(mappedgame, existingGame);
            await _context.SaveChangesAsync();

        }
        public async Task Delete(int id)
        {
            var gameFromDb = await _context.Game.FindAsync(id);
            if (gameFromDb == null)
            {
                throw new Exception($"Game with ID {id} not found.");
            }
            _context.Game.Remove(gameFromDb);
            await _context.SaveChangesAsync();
        }
    }
}
