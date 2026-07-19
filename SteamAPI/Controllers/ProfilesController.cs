using BusinessLogic.DTOs.Profile;
using BusinessLogic.Interfaces;
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
        public async Task<ActionResult<IEnumerable<ProfileDto>>> GetProfiles()
        {
            return Ok(await profilesService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfileDto>> GetProfiles(int id)
        {
            return Ok(await profilesService.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(ProfileDto profile)
        {
            await profilesService.Update(profile.Id, profile);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProfileDto>> Create(ProfileDto profile)
        {
            await profilesService.Create(profile);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            await profilesService.Delete(id);
            return NoContent();
        }
    }
}
