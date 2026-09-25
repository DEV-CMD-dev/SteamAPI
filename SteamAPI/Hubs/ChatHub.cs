using BusinessLogic;
using BusinessLogic.DTOs.Message;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
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

  
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User.GetRequiredUserId();
            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    OnlineUsersStore.OnlineUsers.AddOrUpdate(userId, 1, (userId, count) => count + 1);

                    if (OnlineUsersStore.OnlineUsers[userId] == 1)
                    {
                        await Clients.Others.SendAsync("UserConnected", userId);
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User.GetRequiredUserId();

            try
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    if (OnlineUsersStore.OnlineUsers.TryGetValue(userId, out int count))
                    {
                        if (count > 1)
                        {
                            OnlineUsersStore.OnlineUsers.TryUpdate(userId, count - 1, count);
                        }
                        else
                        {
                            OnlineUsersStore.OnlineUsers.TryRemove(userId, out count);
                            await Clients.Others.SendAsync("UserDisconnected", userId);
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }

            await base.OnDisconnectedAsync(exception);
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
                CreatedAt = message.CreatedAt,
                IsRead = message.IsRead,
            };

            await Clients.User(recepientId).SendAsync("ReceiveMessage", messageDto);
            await Clients.User(senderId).SendAsync("ReceiveMessage", messageDto);
        }

        public async Task ReadMessage(string recepientId)
        {
            var senderId = Context.User.GetRequiredUserId();

            if (string.IsNullOrEmpty(senderId))
                throw new HttpException($"User is not found", HttpStatusCode.NoContent);
            if (string.IsNullOrWhiteSpace(recepientId))
                throw new HttpException($"User is not found", HttpStatusCode.NoContent);

            var messages = await _context.Messages
                .Where(m => m.SenderId == recepientId && m.ReceiverId == senderId)
                .ToListAsync();

            var unreadMessages = messages.Where(m => !m.IsRead).ToList();

            Console.WriteLine(unreadMessages.Count);

            foreach (var message in unreadMessages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();

            await Clients.User(recepientId).SendAsync("MessagesWereRead", senderId);
        }
    }
}
