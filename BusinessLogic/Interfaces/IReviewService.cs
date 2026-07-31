using BusinessLogic.DTOs.Review;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewDto> CreateAsync(CreateReviewDto dto, string userId);

        Task<ReviewDto> GetByIdAsync(int reviewId);

        Task<PaginatedList<ReviewDto>> GetByGameAsync(int gameId, int pageNumber = 1, int pageSize = 10);

        Task<ReviewDto> UpdateAsync(int reviewId, UpdateReviewDto dto, string userId);

        Task DeleteAsync(int reviewId, string userId);
    }
}
