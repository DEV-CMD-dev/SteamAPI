using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.GameVersion;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameVersionController : ControllerBase
    {
        private readonly IGameVersionService _gameVersionService;

        public GameVersionController(IGameVersionService gameVersionService)
        {
            _gameVersionService = gameVersionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGameVersions(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return Ok(await _gameVersionService.GetAll(pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _gameVersionService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateGameVersionDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _gameVersionService.Create(userId, dto);

            return Created(string.Empty, new { message = "Game version created successfully." });
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(int id, PatchGameVersionDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _gameVersionService.Patch(id, userId, dto);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, PutGameVersionDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _gameVersionService.Put(id, userId, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetRequiredUserId();

            await _gameVersionService.Delete(userId, id);

            return NoContent();
        }
    }
}
