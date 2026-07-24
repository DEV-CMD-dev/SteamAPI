using DataAccess.Data.Entities.DataAccess.Data.Entities;
using DataAccess.Enums;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Entities
{
    public class User : IdentityUser
    {
        public string? Bio { get; set; }
        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastOnline { get; set; }
        public UserVisibility UserVisibility { get; set; }
        public UserRole UserRole { get; set; }
        public decimal WalletBalance { get; set; }

        public List<UserGame> UserGames { get; set; } = new();
        public List<Achievement> Achievements { get; set; } = new();
        public List<Game> DevelopedGames { get; set; } = new();
        public List<Wishlist> Wishlists { get; set; } = new();
        public List<Cart> Carts { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();

        public virtual Profile? Profile { get; set; }
    }
}