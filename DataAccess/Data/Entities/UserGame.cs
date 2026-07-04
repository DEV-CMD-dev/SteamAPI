namespace DataAccess.Data.Entities
{
    public class UserGame
    {
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; } 
        public DateTime PurchasedAt { get; set; } = DateTime.UtcNow;
        public int PlayTimeMinutes { get; set; } = 0;
        public bool IsInstalled { get; set; } = false;
    }
}
