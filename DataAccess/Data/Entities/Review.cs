using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    namespace DataAccess.Data.Entities
    {
        public class Review
        {
            public int Id { get; set; }

            public string UserId { get; set; } = string.Empty;
            public User? User { get; set; }

            public int GameId { get; set; }
            public Game? Game { get; set; }
            public bool IsRecommended { get; set; }
            public string Content { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; set; }
        }
    }
}
