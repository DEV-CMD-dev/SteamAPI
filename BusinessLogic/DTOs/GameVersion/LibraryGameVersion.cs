using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.GameVersion
{
    public class LibraryGameVersionDto
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        public string GameImageUrl { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string? PatchNotes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
