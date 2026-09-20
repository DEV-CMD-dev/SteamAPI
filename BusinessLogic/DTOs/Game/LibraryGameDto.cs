using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.Game
{
    public class LibraryGameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public string CoverImageVertical { get; set; } = string.Empty;
        public DateTime LastPlayDate { get; set; }
        public int PlayTimeMinutes { get; set; }
        public bool IsInstalled { get; set; }
    }
}
