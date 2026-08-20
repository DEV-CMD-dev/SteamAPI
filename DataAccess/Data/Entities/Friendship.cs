using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class Friendship
    {
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public string FriendId { get; set; } = string.Empty;
        public User Friend { get; set; } = null!;

        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
