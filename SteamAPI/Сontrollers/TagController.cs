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
    public async Task<IActionResult> GetAllTags()
    {
        return Ok(await _tagService.GetAll());
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTagDto dto)
    {
        await _tagService.Update(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _tagService.Delete(id);

        return NoContent();
    }
}