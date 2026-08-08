using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Game
{
    public class PatchGameDto
    {
        public string? Title { get; set; } 
        public string? Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; } = 5;
        public int? Discount { get; set; }
        public string? SystemRequirements { get; set; } 
        public string? CoverImage { get; set; }
    }
}
