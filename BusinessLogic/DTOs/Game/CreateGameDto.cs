using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Game
{
    public class CreateGameDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string? SystemRequirements { get; set; }
        public IFormFile? CoverImageVertical { get; set; }
        public IFormFile? CoverImageHorizontal { get; set; }
    }
}
