using Microsoft.AspNetCore.Identity;

namespace DataAccess.Data.Entities
{
    public class User : IdentityUser
    {
        public string? Avatar { get; set; }
        public string? Bio { get; set; }
        public string? Country { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateTime LastOnline { get; set; }
        //public UserVisibility UserVisibility { get; set; }
        //public UserRole UserRole { get; set; }
        public decimal WalletBalance { get; set; }

        public List<UserGame> OwnedGames { get; set; } = new();
        public List<Game> DevelopedGames { get; set; } = new();
    }
}
