namespace DataAccess.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public string? HeaderImage { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.UtcNow;

        public string DeveloperId { get; set; } = string.Empty;
        public User Developer { get; set; } = null!;

        public List<UserGame> UserGames { get; set; } = new();
        public List<GameTag> GameTags { get; set; } = new();
    }
}