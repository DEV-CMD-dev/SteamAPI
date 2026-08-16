using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services;

public class WishListService : IWishListService
{
    private readonly SteamDbContext _context;
    private readonly IMapper _mapper;

    public WishListService(SteamDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GameDto>> GetByUserId(string userId)
    {
        return await _context.Wishlists
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .Select(g => g.Game)
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task AddGameToWishlist(string userId, int gameId)
    {
        var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
        if (!gameExists)
            throw new HttpException($"Game with id {gameId} not found", HttpStatusCode.NotFound);
        
        var alreadyExists = await _context.Wishlists
            .AnyAsync(w => w.UserId == userId && w.GameId == gameId);

        if (alreadyExists)
            throw new HttpException($"Game is already in your wishlist", HttpStatusCode.Conflict);
        
        var wishListGame = new Wishlist
        {
            GameId = gameId,
            UserId = userId
        };

        await _context.Wishlists.AddAsync(wishListGame);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveGameFromWishlist(string userId, int gameId)
    {
        var gameExists = await _context.Games.AnyAsync(g => g.Id == gameId);
        if (!gameExists)
            throw new HttpException($"Game with id {gameId} not found", HttpStatusCode.NotFound);
        
        var rowsAffected = await _context.Wishlists
            .Where(w => w.UserId == userId && w.GameId == gameId)
            .ExecuteDeleteAsync();
        
        if (rowsAffected == 0)
            throw new HttpException($"Game is not in your wishlist", HttpStatusCode.NotFound);
    }

}