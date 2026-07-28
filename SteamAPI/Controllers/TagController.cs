using BusinessLogic.DTOs.Tag;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTags(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await _tagService.GetAll(pageNumber, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _tagService.GetById(id));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateTagDto dto)
    {
        var userId = User.GetRequiredUserId();

        await _tagService.Create(userId,dto);

        return Created(string.Empty, new { message = "Tag created successfully." });
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Patch(int id, PatchTagDto dto)
    {
        var userId = User.GetRequiredUserId();

        await _tagService.Patch(userId, id, dto);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, PutTagDto dto)
    {
        var userId = User.GetRequiredUserId();

        await _tagService.Put(userId, id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.GetRequiredUserId();

        await _tagService.Delete(userId,id);

        return NoContent();
    }
}