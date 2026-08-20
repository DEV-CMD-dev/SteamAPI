using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SteamAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        [HttpGet("friends")]
        public async Task<IActionResult> GetFriends(int pageNumber = 1, int pageSize = 10)
        {
            var userId = User.GetRequiredUserId();
            var friends = await _friendshipService.GetFriends(userId, pageNumber, pageSize);
            return Ok(friends);
        }

        [HttpGet("incoming-requests")]
        public async Task<IActionResult> GetIncomingRequests(int pageNumber = 1, int pageSize = 10)
        {
            var userId = User.GetRequiredUserId();
            var requests = await _friendshipService.GetIncomingRequests(userId, pageNumber, pageSize);
            return Ok(requests);
        }

        [HttpPost("send-request")]
        public async Task<IActionResult> SendFriendRequest(string userName)
        {
            var userId = User.GetRequiredUserId();
            await _friendshipService.SendFriendRequest(userId, userName);
            return NoContent();
        }

        [HttpPost("accept-request")]
        public async Task<IActionResult> AcceptFriendRequest(string friendId)
        {
            var userId = User.GetRequiredUserId();
            await _friendshipService.AcceptFriendRequest(userId, friendId);
            return NoContent();
        }

        [HttpPost("decline-request")]
        public async Task<IActionResult> DeclineFriendRequest(string friendId)
        {
            var userId = User.GetRequiredUserId();
            await _friendshipService.DeclineFriendRequest(userId, friendId);
            return NoContent();
        }

        [HttpPost("remove-friend")]
        public async Task<IActionResult> RemoveFriend(string friendId)
        {
            var userId = User.GetRequiredUserId();
            await _friendshipService.RemoveFriend(userId, friendId);
            return NoContent();
        }
    }
}
