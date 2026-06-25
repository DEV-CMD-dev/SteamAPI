using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;



namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGamesService gamesService;
        private readonly IBlobService _blobService;
        private readonly IConfiguration _config;
        public GameController(IGamesService gamesService , IConfiguration config, IBlobService blobService)
        {
            this.gamesService = gamesService;
            _config = config;
            _blobService = blobService;

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
        public async Task<IActionResult> AddGame([FromForm] CreateGameDto model)
        {
            if (model.CoverImage == null || model.CoverImage.Length == 0)
                return BadRequest("Файл обкладинки не вибрано.");
            var imageUrl = await _blobService.UploadBlobAsync(model.CoverImage);

            var result = await gamesService.Create(model, imageUrl);

            return Ok(result);
        }

        [HttpPut("UpdateGame")]
        public async Task<IActionResult> UpdateGame([FromForm] EditGameDto model)
        {
            if (model.CoverImage == null || model.CoverImage.Length == 0)
                return BadRequest("Файл обкладинки не вибрано.");
            var imageUrl = await _blobService.UploadBlobAsync(model.CoverImage);
            await gamesService.Update(model, imageUrl);
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
