using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
