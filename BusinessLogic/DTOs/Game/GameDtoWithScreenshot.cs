using BusinessLogic.DTOs.Screenshot;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTOs.Game
{
    public class GameDtoWithScreenshot
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DeveloperId { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public int Discount { get; set; }
        public string SystemRequirements { get; set; } = string.Empty;
        public string? CoverImageVertical { get; set; } = string.Empty;
        public string? CoverImageHorizontal { get; set; } = string.Empty;
        public List<int>? TagIds { get; set; }
        public List<ScreenshotDto>? Screenshots { get; set; }
    }
}
