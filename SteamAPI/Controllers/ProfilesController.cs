using BusinessLogic.DTOs.Profile;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ProfileDto>> GetById(string userId)
        {
            return Ok(await _profileService.GetById(userId));
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> Patch(PatchProfileDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _profileService.Patch(userId, dto);
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put(PutProfileDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _profileService.Put(userId, dto);
            return Ok();
        }
    }
}
