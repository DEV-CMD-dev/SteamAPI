using BusinessLogic.DTOs.Game;

namespace BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<GameDto>> GetByUserId(string userId);
        Task AddGameToCart(string userId, int gameId);
        Task RemoveGameFromCart(string userId, int gameId);
    }
}
