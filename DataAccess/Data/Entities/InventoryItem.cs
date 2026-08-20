using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class InventoryItem
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; } = 1;
    }
}
