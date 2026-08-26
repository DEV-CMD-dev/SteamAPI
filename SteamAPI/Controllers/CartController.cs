using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyCart()
        {
            var userId = User.GetRequiredUserId();
            var cart = await _cartService.GetByUserId(userId);
            return Ok(cart);
        }

        [HttpPost("{gameId}")]
        public async Task<IActionResult> AddGame(int gameId)
        {
            var userId = User.GetRequiredUserId();

            await _cartService.AddGameToCart(userId, gameId);

            return NoContent();
        }

        [HttpDelete("{gameId}")]
        public async Task<IActionResult> RemoveGame(int gameId)
        {
            var userId = User.GetRequiredUserId();

            await _cartService.RemoveGameFromCart(userId, gameId);

            return NoContent();
        }
    }
}
