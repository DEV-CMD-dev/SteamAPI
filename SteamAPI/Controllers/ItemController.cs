using BusinessLogic.DTOs.Item;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return Ok(await _itemService.GetAll(pageNumber, pageSize));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _itemService.GetById(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateItemDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _itemService.Create(userId, dto);

            return Created(string.Empty, new { message = "Item created successfully." });
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> Patch(int id, PatchItemDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _itemService.Patch(userId, id, dto);

            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, PutItemDto dto)
        {
            var userId = User.GetRequiredUserId();

            await _itemService.Put(userId, id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.GetRequiredUserId();

            await _itemService.Delete(userId, id);

            return NoContent();
        }
    }
}
