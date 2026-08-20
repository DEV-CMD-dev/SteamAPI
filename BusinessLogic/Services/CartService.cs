using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;

        public CartService(SteamDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameDto>> GetByUserId(string userId)
        {
            return await _context.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(g => g.Game)
                .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task AddGameToCart(string userId, int gameId)
        {
            var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
            if (!gameExists)
                throw new HttpException($"Game with id {gameId} not found", HttpStatusCode.NotFound);

            var alreadyExists = await _context.Carts
                .AnyAsync(c => c.UserId == userId && c.GameId == gameId);

            if (alreadyExists)
                throw new HttpException($"Game is already in your cart", HttpStatusCode.Conflict);

            var cartGame = new Cart
            {
                GameId = gameId,
                UserId = userId
            };

            await _context.Carts.AddAsync(cartGame);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveGameFromCart(string userId, int gameId)
        {
            var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
            if (!gameExists)
                throw new HttpException($"Game with id {gameId} not found", HttpStatusCode.NotFound);

            var rowsAffected = await _context.Carts
                .Where(w => w.UserId == userId && w.GameId == gameId)
                .ExecuteDeleteAsync();

            if (rowsAffected == 0)
                throw new HttpException($"Game is not in your cart", HttpStatusCode.NotFound);
        }
    }
}
