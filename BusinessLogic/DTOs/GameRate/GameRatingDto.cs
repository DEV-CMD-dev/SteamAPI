using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Enums;

namespace BusinessLogic.DTOs.GameRate
{
    public sealed class GameRatingDto
    {
        public int TotalReviews { get; set; }

        public int RecommendedReviews { get; set; }

        public int NotRecommendedReviews { get; set; }

        public double RecommendationPercentage { get; set; }

        public GameRating Rating { get; set; }

        public string RatingText { get; set; } = string.Empty;

    }
}
