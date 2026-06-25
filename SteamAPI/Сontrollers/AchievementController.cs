using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementService achievementService;

        public AchievementController(IAchievementService achievementService)
        {
            this.achievementService = achievementService;
        }
        [HttpGet("GetAllAchievement")]
        public async Task<IActionResult> GetAllGames()
        {
            return Ok(await achievementService.GetAll());
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await achievementService.Get(id);
            if (result == null)
            {
                return NotFound($"Game with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost("AddAchievement")]
        public async Task<IActionResult> AddGame(CreateAchivementDto model)
        {
            var result = await achievementService.Create(model);
            if (result == null)
            {
                return BadRequest("Failed to create game.");
            }
            return Ok(result);
        }

        [HttpPut("UpdateAchievement")]
        public async Task<IActionResult> UpdateGame(AchievementDto model)
        {
            await achievementService.Update(model);
            return Ok();
        }

        [HttpDelete("RemoveAchievement")]
        public async Task<IActionResult> RemoveGame(int id)
        {
            await achievementService.Delete(id);
            return Ok();
        }
    }
}
