using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public int GameId { get; set; }

        public bool IsRecommended { get; set; }

        public string Content { get; set; } = string.Empty;

        public double HoursPlayed { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool IsEdited => UpdatedAt.HasValue;
    }
}
