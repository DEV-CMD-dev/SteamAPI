using Microsoft.AspNetCore.Identity;
//using Shared.Enums;

namespace DataAccess.Data.Entities
{
    public class User : IdentityUser
    {
        public string? Avatar { get; set; }
        public string? Bio { get; set; }
        public string? Country { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateTime LastOnline { get; set; }
        //public UserVisibility Status { get; set; }
        public List<OwnedGame> OwnedGames { get; set; } = [];
        public List<UserAchievement> UserAchievements { get; set; } = [];
    }
}
