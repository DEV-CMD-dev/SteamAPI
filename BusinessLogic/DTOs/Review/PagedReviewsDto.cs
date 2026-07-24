using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Review
{
    public sealed class PagedReviewsDto
    {
        public IEnumerable<ReviewDto> Reviews { get; set; } = Array.Empty<ReviewDto>();

        public int TotalCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
