using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Services
{
    public class GameRatingService : IGameRatingService
    {
        private readonly SteamDbContext _context;

        public GameRatingService(SteamDbContext context)
        {
            _context = context;
        }

        public async Task UpdateGameRatingAsync(int gameId)
        {
            var game = await _context.Games
                .FirstOrDefaultAsync(g => g.Id == gameId);

            if (game == null)
                throw new HttpException("Game not found", HttpStatusCode.NotFound);

            var totalReviews = await _context.Reviews
                .CountAsync(r => r.GameId == gameId);

            var recommendedReviews = await _context.Reviews
                .CountAsync(r =>
                    r.GameId == gameId &&
                    r.Recommendation == ReviewRecommendation.Recommended);

            decimal recommendationPercentage = 0;

            if (totalReviews > 0)
            {
                recommendationPercentage =
                    Math.Round((decimal)recommendedReviews * 100 / totalReviews, 2);
            }

            game.TotalReviews = totalReviews;
            game.RecommendedReviews = recommendedReviews;
            game.RecommendationPercentage = recommendationPercentage;
            game.Rating = CalculateRating(totalReviews, recommendationPercentage);

            await _context.SaveChangesAsync();
        }

        private static GameRating CalculateRating(
            int totalReviews,
            decimal recommendationPercentage)
        {
            if (totalReviews < 10)
                return GameRating.None;

            if (recommendationPercentage >= 95)
                return GameRating.OverwhelminglyPositive;

            if (recommendationPercentage >= 80)
                return GameRating.VeryPositive;

            if (recommendationPercentage >= 70)
                return GameRating.MostlyPositive;

            if (recommendationPercentage >= 40)
                return GameRating.Mixed;

            if (recommendationPercentage >= 30)
                return GameRating.MostlyNegative;

            if (recommendationPercentage >= 20)
                return GameRating.Negative;

            return GameRating.OverwhelminglyNegative;
        }
    }
}