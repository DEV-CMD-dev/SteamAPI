using BusinessLogic.DTOs.Game;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Сontrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpGet("getGames")]
        public async Task<IActionResult> GetAllGames()
        {
            try
            {
                var result = await _gameService.GetAll();
                return Ok(result);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getGameById")]
        public async Task<IActionResult> GetById(int id)
        {

            try
            {
                var result = await _gameService.Get(id);
                return Ok(result);
            }
            catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
           
        }
        [HttpPost("addGame")]
        [Authorize]
        public async Task<IActionResult> AddGame(CreateGameDto dto)
        {
            
            try
            {
                var result = await _gameService.Create(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("updateGame")]
        [Authorize]
        public async Task<IActionResult> UpdateGame(UpdateGameDto dto)
        {
            try
            {
                await _gameService.Update(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("deleteGame")]
        [Authorize]
        public async Task<IActionResult> DeleteGame(DeleteGameDto dto)
        {
            

            try
            {
                await _gameService.Delete(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
           
        }
    }
}
