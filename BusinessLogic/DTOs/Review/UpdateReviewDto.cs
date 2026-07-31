using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Review
{
    public class UpdateReviewDto
    {
        public bool IsRecommended { get; set; }

        [Required(ErrorMessage = "Review content cannot be empty")]
        [MaxLength(2000, ErrorMessage = "Review content cannot exceed 2000 characters")]
        public string Content { get; set; } = string.Empty;
    }
}