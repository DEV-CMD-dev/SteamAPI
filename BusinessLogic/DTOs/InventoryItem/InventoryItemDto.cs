using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.InventoryItem
{
    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ItemId { get; set; }

        public DateTime AcquiredAt { get; set; }
        public int Quantity { get; set; }
    }
}
