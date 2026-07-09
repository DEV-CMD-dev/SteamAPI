
namespace DataAccess.Data.Entities
{
    public class GameVersion
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public string Version { get; set; }
        public string? PatchNotes { get; set; }
    }
}
