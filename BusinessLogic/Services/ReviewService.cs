using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Review;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using DataAccess.Data.Entities.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace BusinessLogic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly SteamDbContext _context;
        private readonly IGameRatingService _gameRatingService;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public ReviewService(
            SteamDbContext context,
            IGameRatingService gameRatingService,
            IMapper mapper,
            IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _gameRatingService = gameRatingService;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }

        private async Task<ReviewDto> MapReviewAsync(Review review)
        {
            var dto = _mapper.Map<ReviewDto>(review);

            dto.HoursPlayed = await _context.UserGames
                .Where(x => x.UserId == review.UserId &&
                            x.GameId == review.GameId)
                .Select(x => Math.Round(x.PlayTimeMinutes / 60.0, 1))
                .FirstOrDefaultAsync();

            return dto;
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

            var review = new Review
            {
                UserId = userId,
                GameId = dto.GameId,
                IsRecommended = dto.IsRecommended,
                Content = dto.Content.Trim()
            };

            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();
            await _gameRatingService.UpdateGameRatingAsync(review.GameId);

            await _context.Entry(review).Reference(x => x.User).LoadAsync();

            return await MapReviewAsync(review);
        }

        public async Task<ReviewDto> GetByIdAsync(int reviewId)
        {
            if (reviewId <= 0)
                throw new HttpException(
                    "Invalid review ID",
                    HttpStatusCode.BadRequest);

            var review = await _context.Reviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == reviewId);

            if (review == null)
                throw new HttpException(
                    "Review not found",
                    HttpStatusCode.NotFound);

            return await MapReviewAsync(review);
        }

        public async Task<PaginatedList<ReviewDto>> GetByGameAsync(
            int gameId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            if (gameId <= 0)
                throw new HttpException(
                    "Invalid game ID",
                    HttpStatusCode.BadRequest);

            var gameExists = await _context.Games
                 .AnyAsync(g => g.Id == gameId);

            if (!gameExists)
                throw new HttpException(
                   "Game not found",
                   HttpStatusCode.NotFound);

            var query = _context.Reviews
                .AsNoTracking()
                .Where(r => r.GameId == gameId)
                .OrderByDescending(r => r.CreatedAt)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider);

            return await PaginatedList<ReviewDto>.CreateAsync(
                query,
                pageNumber,
                pageSize,
                _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<ReviewDto> UpdateAsync(
            int reviewId,
            UpdateReviewDto dto,
            string userId)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
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

            review.IsRecommended = dto.IsRecommended;
            review.Content = dto.Content.Trim();
            review.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            await _gameRatingService.UpdateGameRatingAsync(review.GameId);

            return await MapReviewAsync(review);
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