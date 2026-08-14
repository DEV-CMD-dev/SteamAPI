
namespace DataAccess.Data.Entities
{
    public class Profile
    {
        public string UserId { get; set; }
        public string? Avatar { get; set; }
        public User? User { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }
        public string? Badges { get; set; }
        public string? Showcase { get; set; }
    }
}
