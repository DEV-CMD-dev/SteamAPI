using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService _achievementService;

        public AchievementController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAchievements(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return Ok(await _achievementService.GetAll(pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _achievementService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateAchievementDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _achievementService.Create(userId, dto);

            return Created(string.Empty, new { message = "Achievement created successfully." });
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(int id, PatchAchievementDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _achievementService.Patch(id, userId, dto);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, PutAchievementDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _achievementService.Put(id, userId, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetRequiredUserId();

            await _achievementService.Delete(userId, id);

            return NoContent();
        }
    }
}
