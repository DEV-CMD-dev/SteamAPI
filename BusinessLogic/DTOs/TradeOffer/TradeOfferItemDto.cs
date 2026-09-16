using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.TradeOffer
{
    public class TradeOfferItemDto
    {
        public int InventoryItemId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string GameTitle { get; set; }
    }
}
