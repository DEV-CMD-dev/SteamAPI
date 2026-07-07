
namespace DataAccess.Data.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }
        public string Badges { get; set; }
        public string Showcase { get; set; }
    }
}
