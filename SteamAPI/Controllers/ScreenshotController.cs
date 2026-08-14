using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.Screenshot;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenshotController : ControllerBase
    {
        private readonly IScreenshotService _screenshotService;

        public ScreenshotController(IScreenshotService screenshotService)
        {
            _screenshotService = screenshotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScreenshots(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return Ok(await _screenshotService.GetAll(pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _screenshotService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateScreenshotDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _screenshotService.Create(userId, dto);

            return Created(string.Empty, new { message = "Screenshot created successfully." });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateScreenshotDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _screenshotService.Update(id, userId, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetRequiredUserId();

            await _screenshotService.Delete(userId, id);

            return NoContent();
        }
    }
}
