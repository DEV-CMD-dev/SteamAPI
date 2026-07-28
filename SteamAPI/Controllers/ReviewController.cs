using BusinessLogic.DTOs.Review;
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

        [HttpGet("{reviewId:int}")]
        public async Task<ActionResult<ReviewDto>> GetById(int reviewId)
        {
            var review = await _reviewService.GetByIdAsync(reviewId);

            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpGet("game/{gameId:int}")]
        public async Task<ActionResult<PaginatedList<ReviewDto>>> GetByGame(
            int gameId,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result = await _reviewService.GetByGameAsync(
                gameId,
                pageNumber,
                pageSize);

            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Create(CreateReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var review = await _reviewService.CreateAsync(dto, userId);

            return CreatedAtAction(nameof(GetById), new { reviewId = review.Id }, review);
        }


        [Authorize]
        [HttpPut("{reviewId:int}")]
        public async Task<ActionResult<ReviewDto>> Update(
            int reviewId,
            UpdateReviewDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var review = await _reviewService.UpdateAsync(reviewId, dto, userId);

            return Ok(review);
        }

        [Authorize]
        [HttpDelete("{reviewId:int}")]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await _reviewService.DeleteAsync(reviewId, userId);

            return NoContent();
        }
    }
}