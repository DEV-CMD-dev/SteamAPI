using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.GameVersion
{
    public class CreateGameVersionDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Game ID must be greater than zero")]
        public int GameId { get; set; }
        public string Version { get; set; }
        public string? PatchNotes { get; set; }
    }
}
