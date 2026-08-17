using BusinessLogic.DTOs.Tag;
using BusinessLogic.DTOs.TradeOffer;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyInventory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.GetRequiredUserId();

            var result = await _tradeService.GetTradeOffers(userId, pageNumber, pageSize);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTrade([FromBody] CreateTradeOfferDto dto)
        {
            var userId = User.GetRequiredUserId();
    
            await _tradeService.CreateTradeOfferAsync(userId, dto);

            return Ok(new { message = "Пропозицію обміну успішно надіслано!" });
        }
    }
}
