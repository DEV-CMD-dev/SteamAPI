namespace BusinessLogic.DTOs.GameVersion
{
    public class GameVersionDto
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Version { get; set; }
        public string? PatchNotes { get; set; }
    }
}
