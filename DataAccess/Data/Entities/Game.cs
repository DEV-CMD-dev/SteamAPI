namespace DataAccess.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? DeveloperId { get; set; }
        public User? Developer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string SystemRequirements { get; set; }
        public string? CoverImage { get; set; }

        public List<Tag>? Tags { get; set; }
        public List<Screenshot>? Screenshots { get; set; }
        public List<GameVersion>? Versions { get; set; }
        public List<Achievement>? Achievements { get; set; }
        public List<UserGame>? UserGames { get; set; }
    }
}