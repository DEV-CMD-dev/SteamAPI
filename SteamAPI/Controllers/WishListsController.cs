using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Extensions;

namespace SteamAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class WishListsController : ControllerBase
{
    private readonly IWishListService _wishListService;

    public WishListsController(IWishListService wishListService)
    {
        _wishListService = wishListService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMyWishlist()
    {
        var userId = User.GetRequiredUserId();
        var wishlist = await _wishListService.GetByUserId(userId);
        return Ok(wishlist);
    }
    
    [HttpPost("{gameId}")]
    public async Task<IActionResult> AddGame(int gameId)
    {
        var userId = User.GetRequiredUserId();

        await _wishListService.AddGameToWishlist(userId, gameId);

        return NoContent();
    }
    
    [HttpDelete("{gameId}")]
    public async Task<IActionResult> RemoveGame(int gameId)
    {
        var userId = User.GetRequiredUserId();

        await _wishListService.RemoveGameFromWishlist(userId, gameId);

        return NoContent();
    }
}