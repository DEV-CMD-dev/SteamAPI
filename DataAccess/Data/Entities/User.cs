using DataAccess.Enums;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Entities
{
    public class User : IdentityUser
    {
        public string? Avatar { get; set; }
        public string? Bio { get; set; }
        public string? Country { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastOnline { get; set; }
        public UserVisibility UserVisibility { get; set; }
        public UserRole UserRole { get; set; }
        public decimal WalletBalance { get; set; }
        public List<UserGame> UserGames { get; set; } = [];
        public List<Achievement> Achievements { get; set; } = [];
        public List<Game> DevelopedGames { get; set; } = [];
    }
}
