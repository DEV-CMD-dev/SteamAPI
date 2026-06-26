namespace DataAccess.Data.Entities
{
    public class GameTag
    {
        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }
}