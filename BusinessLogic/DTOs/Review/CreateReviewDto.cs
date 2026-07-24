using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTOs.Review
{
    public class CreateReviewDto
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public ReviewRecommendation Recommendation { get; set; }

        [Required]
        [MaxLength(8000)]
        public string Content { get; set; } = string.Empty;
    }
}
