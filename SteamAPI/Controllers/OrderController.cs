using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetOrders()
        {
            var userId = User.GetRequiredUserId();
            var orders = await _orderService.GetOrders(userId);
            return Ok(orders);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.GetRequiredUserId();
            await _orderService.Checkout(userId);
            return Ok();
        }
    }
}
