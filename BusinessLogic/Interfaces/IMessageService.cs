using BusinessLogic.DTOs.Message;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IMessageService
    {
        Task<PaginatedList<MessageDto>> GetMessages(string senderId, string receiverId, int pageNumber, int pageSize);
    }
}
