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

        [HttpGet("GetMyProfile")]
        [Authorize]
        public async Task<ActionResult<MiniProfileDto>> GetMyProfile()
        {
            var userId = User.GetRequiredUserId();
            return Ok(await _profileService.GetMyProfile(userId));
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
        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var result = await _profileService.SearchByUserName(query);

            return Ok(result);
        }

        [HttpGet("search-friends")]
        [Authorize]
        public async Task<IActionResult> SearchFriends([FromQuery] string query)
        {
            var user = User.GetRequiredUserId();
            var result = await _profileService.SearchFriendsByUserName(query, user);

            return Ok(result);
        }
    }
}
