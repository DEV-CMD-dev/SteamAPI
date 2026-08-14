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
        private readonly IProfileService profilesService;

        public ProfilesController(IProfileService profilesService)
        {
            this.profilesService = profilesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetProfiles(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return Ok(await profilesService.GetAll(pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetProfiles(int id)
        {
            return Ok(await profilesService.GetById(id));
        }

        [HttpPatch()]
        [Authorize]
        public async Task<IActionResult> Patch(PatchProfileDto dto)
        {
            var userId = User.GetRequiredUserId();

            await profilesService.Patch(userId, dto);
            return Ok();
        }

        [HttpPut()]
        [Authorize]
        public async Task<IActionResult> Put(PutProfileDto dto)
        {
            var userId = User.GetRequiredUserId();

            await profilesService.Put(userId, dto);
            return Ok();
        }
    }
}
