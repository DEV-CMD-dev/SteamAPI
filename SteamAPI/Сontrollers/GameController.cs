using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;


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
        [HttpGet("GetAllGames")]
        public async Task<IActionResult> GetAllGames()
        {
            return Ok(await gamesService.GetAll());
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await gamesService.Get(id);
            if (result == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame(CreateGameDto model)
        {
            var result = await gamesService.Create(model);
            if(result == null)
            {
                return BadRequest("Failed to create game.");
            }
            return Ok(result);
        }

        [HttpPut("UpdateGame")]
        public async Task<IActionResult> UpdateGame(GameDto model)
        {
            await gamesService.Update(model);
            return Ok();
        }

        [HttpDelete("RemoveGame")]
        public async Task<IActionResult> RemoveGame(int id)
        {
            await gamesService.Delete(id);
            return Ok();
        }
    }
}
