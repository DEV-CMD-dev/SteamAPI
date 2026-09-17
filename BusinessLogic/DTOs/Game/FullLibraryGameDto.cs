using BusinessLogic.DTOs.Screenshot;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogic.DTOs.Game
{
    public class FullLibraryGameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string DeveloperId { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string? CoverImageVertical { get; set; } = string.Empty;
        public string? CoverImageHorizontal { get; set; } = string.Empty;
        public string? IconUrl { get; set; } = string.Empty;
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastPlayDate { get; set; } = DateTime.UtcNow;
        public int PlayTimeMinutes { get; set; } = 0;
        public bool IsInstalled { get; set; } = false;
    }
}
