
namespace DataAccess.Data.Entities
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public string? IconUrl { get; set; }

        public List<User>? Users { get; set; }
    }
}
