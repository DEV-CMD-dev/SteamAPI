using BusinessLogic.DTOs.Tag;
using BusinessLogic.Interfaces;
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
    public async Task<IActionResult> GetAllTags([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        return Ok(await _tagService.GetAll(pageNumber,pageSize));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _tagService.GetById(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTagDto dto)
    {
        await _tagService.Create(dto);

        return Created(string.Empty, new { message = "Tag created successfully." });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, PatchTagDto dto)
    {
        await _tagService.Patch(id, dto);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PutTagDto dto)
    {
        await _tagService.Put(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _tagService.Delete(id);

        return NoContent();
    }
}