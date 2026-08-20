using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data.Entities
{
    [Index(nameof(Price))]
    [Index(nameof(Discount))]
    [Index(nameof(Title))]
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? DeveloperId { get; set; }
        public User? Developer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
        public string? SystemRequirements { get; set; }
        public string? CoverImageVertical { get; set; }
        public string? CoverImageHorizontal { get; set; }
        public int Discount { get; set; }

        public List<Tag>? Tags { get; set; }
        public List<Screenshot>? Screenshots { get; set; }
        public List<GameVersion>? Versions { get; set; }
        public List<Achievement>? Achievements { get; set; }
        public List<UserGame>? UserGames { get; set; }
        public List<Wishlist> Wishlists { get; set; } = new();
        public List<Cart> Carts { get; set; } = new();
        public List<Item> Items { get; set; }
    }
}