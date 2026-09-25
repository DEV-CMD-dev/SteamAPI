using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Message
{
    public class FriendMessageDto
    {
        public string UserId { get; set; }
        public string? Avatar { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsOnline { get; set; }
        public string? LastMessage { get; set; }
        public int UnreadMessageCounter { get; set; }
    }
}
