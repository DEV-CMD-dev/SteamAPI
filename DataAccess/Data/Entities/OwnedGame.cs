using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class OwnedGame
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public User? User { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public TimeSpan PlayTime { get; set; }
        public DateOnly LastPlayed { get; set; }
    }
}
