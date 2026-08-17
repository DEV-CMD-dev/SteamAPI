using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemController : ControllerBase
    {
        private readonly IInventoryItemService _inventoryItemService;

        public InventoryItemController(IInventoryItemService inventoryItemService)
        {
            _inventoryItemService = inventoryItemService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyInventory([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.GetRequiredUserId();

            var result = await _inventoryItemService.GetAll(userId, pageNumber, pageSize);

            return Ok(result);
        }

        [HttpPost("buy/{itemId}")]
        [Authorize]
        public async Task<IActionResult> BuyItem([FromRoute] int itemId)
        {
            var userId = User.GetRequiredUserId();

            await _inventoryItemService.BuyFromStoreAsync(userId, itemId);

            return Ok(new { message = "Item bought successfully!" });
        }

        [HttpDelete("sell/{inventoryItemId}")]
        [Authorize]
        public async Task<IActionResult> SellItem([FromRoute] int inventoryItemId)
        {
            var userId = User.GetRequiredUserId();

            await _inventoryItemService.SellFromInventoryAsync(userId, inventoryItemId);

            return Ok(new { message = "Item sold successfully!" });
        }



    }
}
