using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserGames()
        {
            var userId = User.GetRequiredUserId();
            var games = await _libraryService.GetUserGames(userId);
            return Ok(games);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int gameId)
        {
            var games = await _libraryService.GetById(gameId);
            return Ok(games);
        }
    }
}
