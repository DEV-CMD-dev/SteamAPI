using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Extensions;
using BusinessLogic.Extensions.SearchFilters;

[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;

    public GamesController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames(
        [FromQuery] GameParameters gameParams,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool withScreenshots = false)
    {
        return Ok(await _gameService.GetAll(pageNumber, pageSize, gameParams, withScreenshots));
    }

    [HttpGet("library")]
    [Authorize]
    public async Task<IActionResult> GetUserLibrary(
        [FromQuery] GameParameters gameParams,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] bool withScreenshots = false)
    {
        var userId = User.GetRequiredUserId();
        return Ok(await _gameService.GetUserLibrary(userId, pageNumber, pageSize, gameParams, withScreenshots));
    }

    [HttpGet("top-sellers")]
    public async Task<IActionResult> GetTopSellers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await _gameService.GetTopSellers(pageNumber, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _gameService.GetById(id));
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add(CreateGameDto dto)
    {
        var userId = User.GetRequiredUserId();

        var result = await _gameService.Create(userId, dto);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, PutGameDto dto)
    {
        var userId = User.GetRequiredUserId();

        await _gameService.Put(id, userId, dto);

        return NoContent();
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Patch(int id, PatchGameDto dto)
    {
        var userId = User.GetRequiredUserId();

        await _gameService.Patch(id, userId, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.GetRequiredUserId();

        await _gameService.Delete(id, userId);

        return NoContent();
    }
}