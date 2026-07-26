using BusinessLogic;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

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
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);

        await _tagService.Create(userId,dto);

        return Created(string.Empty, new { message = "Tag created successfully." });
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Patch(int id, PatchTagDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);

        await _tagService.Patch(userId, id, dto);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, PutTagDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);

        await _tagService.Put(userId, id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);

        await _tagService.Delete(userId,id);

        return NoContent();
    }
}