using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService tagService;

        public TagController(ITagService tagService)
        {
            this.tagService = tagService;
        }
        [HttpGet("GetAllTag")]
        public async Task<IActionResult> GetAllTags()
        {
            return Ok(await tagService.GetAll());
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await tagService.Get(id);
            if (result == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost("AddTag")]
        public async Task<IActionResult> AddTag(CreateTagDto model)
        {
            var result = await tagService.Create(model);
            if (result == null)
            {
                return BadRequest("Failed to create tag.");
            }
            return Ok(result);
        }

        [HttpPut("UpdateTag")]
        public async Task<IActionResult> UpdateTag(TagDto model)
        {
            await tagService.Update(model);
            return Ok();
        }

        [HttpDelete("RemoveTag")]
        public async Task<IActionResult> RemoveTag(int id)
        {
            await tagService.Delete(id);
            return Ok();
        }
    }
}
