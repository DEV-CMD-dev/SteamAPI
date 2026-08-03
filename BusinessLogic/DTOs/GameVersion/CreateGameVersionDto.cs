namespace BusinessLogic.DTOs.GameVersion
{
    public class CreateGameVersionDto
    {
        public int GameId { get; set; }
        public string Version { get; set; }
        public string? PatchNotes { get; set; }
    }
}
