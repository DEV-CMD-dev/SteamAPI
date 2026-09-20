using BusinessLogic;
using BusinessLogic.Extensions;
using BusinessLogic.DTOs.Message;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

using System.Net;

namespace SteamAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly SteamDbContext _context;

        public ChatHub(SteamDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string recepientId, string messageText)
        {
            var senderId = Context.User.GetRequiredUserId();

            if (string.IsNullOrEmpty(senderId))
                throw new HttpException($"User is not found", HttpStatusCode.NoContent);
            if (string.IsNullOrWhiteSpace(messageText))
                throw new HttpException($"Message is empty", HttpStatusCode.NoContent);
            if (string.IsNullOrWhiteSpace(recepientId))
                throw new HttpException($"User is not found", HttpStatusCode.NoContent);

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = recepientId,
                Text = messageText,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            var messageDto = new MessageDto
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Text = message.Text,
                CreatedAt =message.CreatedAt
            };

            await Clients.User(recepientId).SendAsync("ReceiveMessage", messageDto);
            await Clients.User(senderId).SendAsync("ReceiveMessage", messageDto);
        }
    }
}
