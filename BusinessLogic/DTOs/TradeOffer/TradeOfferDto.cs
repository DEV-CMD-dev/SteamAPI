using DataAccess.Data.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.TradeOffer
{
    public class TradeOfferDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int SenderInventoryItemId { get; set; }
        public int ReceiverInventoryItemId { get; set; }

        public TradeOfferStatus Status { get; set; } = TradeOfferStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
