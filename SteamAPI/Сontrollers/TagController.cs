using BusinessLogic.DTOs.Game;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("getTags")]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                var result = await _tagService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getTag")]
        public async Task<IActionResult> GetTag(int id)
        {
            try
            {
                var result = await _tagService.Get(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createTag")]
        public async Task<IActionResult> CreateTag(CreateTagDto dto)
        {
            try
            {
                await _tagService.Create(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateTag")]
        public async Task<IActionResult> UpdateTag(TagDto dto)
        {
            try
            {
                await _tagService.Update(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteTag")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            try
            {
                await _tagService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
