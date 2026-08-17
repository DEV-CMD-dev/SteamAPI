using BusinessLogic.DTOs.TradeOffer;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTradeOffers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.GetRequiredUserId();

            var result = await _tradeService.GetTradeOffers(userId, pageNumber, pageSize);

            return Ok(result);
        }

    
        [HttpPost("create")]
        public async Task<IActionResult> CreateTrade([FromBody] CreateTradeOfferDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _tradeService.CreateTradeOfferAsync(userId, dto);

            return Ok(new { message = "Trade offer created successfully!" });
        }


        [HttpPost("{id}/accept")]
        public async Task<IActionResult> AcceptTrade([FromRoute] int id)
        {
            var userId = User.GetRequiredUserId();

            await _tradeService.AcceptTradeOfferAsync(userId, id);

            return Ok(new { message = "Trade offer accepted successfully!" });
        }

  
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelTrade([FromRoute] int id)
        {
            var userId = User.GetRequiredUserId();

            await _tradeService.CancelTradeOfferAsync(userId, id);

            return Ok(new { message = "Trade offer cancelled successfully!" });
        }
    }
}