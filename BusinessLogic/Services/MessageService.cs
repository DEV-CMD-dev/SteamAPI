using AutoMapper;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Message;
using BusinessLogic.DTOs.Profile;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class MessageService : IMessageService
    {
        private readonly SteamDbContext _context;
        private readonly FrontendOptions _frontendOptions;

        public MessageService(SteamDbContext context, IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;;
            _frontendOptions = frontendOptions.Value;
        }
        public async Task<PaginatedList<MessageDto>> GetMessages(string senderId, string receiverId, int pageNumber, int pageSize)
        {
            var query = _context.Messages
                .AsNoTracking()
                .Where(m => m.SenderId == senderId && m.ReceiverId == receiverId || m.SenderId == receiverId && m.ReceiverId == senderId)
                .OrderBy(m => m.CreatedAt)
                .Select(m => new MessageDto
                {
                   Id = m.Id,
                   SenderId = m.SenderId,
                   ReceiverId = m.ReceiverId,
                   CreatedAt = m.CreatedAt,
                   Text = m.Text,
                   IsRead = m.IsRead,
                });
            return await PaginatedList<MessageDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }
    }
}
