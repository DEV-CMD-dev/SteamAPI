using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;

namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGamesService gamesService;

        public GameController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllGames()
        {
            return Ok(await gamesService.GetAll());
        }
    }
}
