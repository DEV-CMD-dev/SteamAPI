using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Review
{
    public class CreateReviewDto
    {
        [Required(ErrorMessage = "Game ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid Game ID")]
        public int GameId { get; set; }

        public bool IsRecommended { get; set; }

        [Required(ErrorMessage = "Review content cannot be empty")]
        [MaxLength(2000, ErrorMessage = "Review content cannot exceed 2000 characters")]
        public string Content { get; set; } = string.Empty;
    }
}