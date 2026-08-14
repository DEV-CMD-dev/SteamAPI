using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces;

public interface IWishListService
{
    Task<IEnumerable<GameDto>> GetByUserId(string userId);
    Task AddGameToWishlist(string userId, int gameId);
    Task RemoveGameFromWishlist(string userId, int gameId);
}