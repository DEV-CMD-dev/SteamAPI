using BusinessLogic.Classes;
using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    // TODO: remove duplication in UserIdentity verification
    
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        return Ok(await _gameService.GetAll(pageNumber, pageSize));
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
        var developerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(developerId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);

        var result = await _gameService.Create(developerId, dto);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, PutGameDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);
        
        await _gameService.Put(id, userId, dto);

        return NoContent();
    }

    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Patch(int id, PatchGameDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);
        
        await _gameService.Patch(id, userId, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(currentUserId))
            throw new HttpException(
                "User identity could not be verified.",
                HttpStatusCode.Unauthorized);

        await _gameService.Delete(id, currentUserId);

        return NoContent();
    }
}