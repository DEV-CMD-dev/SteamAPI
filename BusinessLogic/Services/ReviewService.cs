using BusinessLogic.Helpers;
using BusinessLogic.DTOs.Review;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly SteamDbContext _context;
        private readonly IGameRatingService _gameRatingService;

        public ReviewService(
            SteamDbContext context,
            IGameRatingService gameRatingService)
        {
            _context = context;
            _gameRatingService = gameRatingService;
        }

        private IQueryable<ReviewDto> MapToDto()
        {
            return _context.Reviews
                .AsNoTracking()
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    UserName = r.User != null ? r.User.UserName ?? string.Empty : string.Empty,
                    GameId = r.GameId,
                    Recommendation = r.Recommendation,
                    Content = r.Content,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,

                    HoursPlayed = _context.UserGames
                        .Where(ug => ug.UserId == r.UserId && ug.GameId == r.GameId)
                        .Select(ug => (double?)Math.Round(ug.PlayTimeMinutes / 60.0, 1))
                        .FirstOrDefault() ?? 0
                });
        }

        public async Task<ReviewDto> CreateAsync(CreateReviewDto dto, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new HttpException(
                    "User identity could not be verified",
                    HttpStatusCode.Unauthorized);

            if (!await _context.Users.AnyAsync(u => u.Id == userId))
                throw new HttpException(
                    "User not found",
                    HttpStatusCode.NotFound);

            if (!await _context.Games.AnyAsync(g => g.Id == dto.GameId))
                throw new HttpException("Game not found", HttpStatusCode.NotFound);

            if (await _context.Reviews.AnyAsync(r =>
                r.UserId == userId &&
                r.GameId == dto.GameId))
            {
                throw new HttpException(
                    "You have already reviewed this game",
                    HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new HttpException(
                    "Review content cannot be empty",
                    HttpStatusCode.BadRequest);

            if (dto.Content.Length > 8000)
                throw new HttpException(
                    "Review content cannot exceed 8000 characters",
                    HttpStatusCode.BadRequest);

            var review = new Review
            {
                UserId = userId,
                GameId = dto.GameId,
                Recommendation = dto.Recommendation,
                Content = dto.Content.Trim()
            };

            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();
            await _gameRatingService.UpdateGameRatingAsync(review.GameId);

            return await MapToDto()
                .FirstAsync(r => r.Id == review.Id);
        }

        public async Task<ReviewDto?> GetByIdAsync(int reviewId)
        {
            return await MapToDto()
                .FirstOrDefaultAsync(r => r.Id == reviewId);
        }

        public async Task<PaginatedList<ReviewDto>> GetByGameAsync(
            int gameId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var gameExists = await _context.Games
                 .AnyAsync(g => g.Id == gameId);

            if (!gameExists)
                throw new HttpException(
                   "Game not found",
                   HttpStatusCode.NotFound);

            var query = MapToDto()
                .Where(r => r.GameId == gameId)
                .OrderByDescending(r => r.CreatedAt);

            return await PaginatedList<ReviewDto>.CreateAsync(
                query,
                pageNumber,
                pageSize,
                 50);
            }
        

        public async Task<ReviewDto> UpdateAsync(
            int reviewId,
            UpdateReviewDto dto,
            string userId)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            if (string.IsNullOrWhiteSpace(userId))
                throw new HttpException(
                    "User identity could not be verified",
                    HttpStatusCode.Unauthorized);

            if (!await _context.Users.AnyAsync(u => u.Id == userId))
                throw new HttpException(
                    "User not found",
                    HttpStatusCode.NotFound);

            if (review == null)
                throw new HttpException(
                    "Review not found",
                    HttpStatusCode.NotFound);

            if (review.UserId != userId)
                throw new HttpException(
                    "You can edit only your own review",
                    HttpStatusCode.Forbidden);

            if (string.IsNullOrWhiteSpace(dto.Content))
                throw new HttpException(
                    "Review content cannot be empty",
                    HttpStatusCode.BadRequest);

            if (dto.Content.Length > 8000)
                throw new HttpException(
                    "Review content cannot exceed 8000 characters",
                    HttpStatusCode.BadRequest);

            review.Recommendation = dto.Recommendation;
            review.Content = dto.Content.Trim();
            review.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await _gameRatingService.UpdateGameRatingAsync(review.GameId);

            return await MapToDto()
                .FirstAsync(r => r.Id == review.Id);
        }

        public async Task DeleteAsync(int reviewId, string userId)
        {
            var review = await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            if (string.IsNullOrWhiteSpace(userId))
                throw new HttpException(
                    "User identity could not be verified",
                    HttpStatusCode.Unauthorized);

            if (!await _context.Users.AnyAsync(u => u.Id == userId))
                throw new HttpException(
                    "User not found",
                    HttpStatusCode.NotFound);

            if (review == null)
                throw new HttpException(
                    "Review not found",
                    HttpStatusCode.NotFound);

            if (review.UserId != userId)
                throw new HttpException(
                    "You can delete only your own review",
                    HttpStatusCode.Forbidden);

            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();

            await _gameRatingService.UpdateGameRatingAsync(review.GameId);
        }
    }
}