using BusinessLogic.DTOs.Review;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SteamAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewDto>> Create(CreateReviewDto dto)
        {
            var userId = User.GetRequiredUserId();

            var review = await _reviewService.CreateAsync(dto, userId);

            return CreatedAtAction(
                nameof(GetById),
                new { reviewId = review.Id },
                review);
        }

        [HttpGet("{reviewId:int}")]
        public async Task<ActionResult<ReviewDto>> GetById(int reviewId)
        {
            return Ok(await _reviewService.GetByIdAsync(reviewId));
        }

        [HttpGet("game/{gameId:int}")]
        public async Task<ActionResult<PaginatedList<ReviewDto>>> GetByGame(
            int gameId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            return Ok(await _reviewService.GetByGameAsync(gameId, pageNumber, pageSize));
        }

        [HttpPut("{reviewId:int}")]
        [Authorize]
        public async Task<ActionResult<ReviewDto>> Update(
            int reviewId,
            UpdateReviewDto dto)
        {
            var userId = User.GetRequiredUserId();

            return Ok(await _reviewService.UpdateAsync(reviewId, dto, userId));
        }

        [HttpDelete("{reviewId:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var userId = User.GetRequiredUserId();

            await _reviewService.DeleteAsync(reviewId, userId);

            return NoContent();
        }
    }
}