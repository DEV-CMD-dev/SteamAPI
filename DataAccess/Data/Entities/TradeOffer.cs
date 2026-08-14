using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Data.Entities
{
    public class TradeOffer
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }
        public string ReceiverId { get; set; }
        public User Receiver { get; set; }

        public int SenderItemTemplateId { get; set; }
        public int SenderItemAmount { get; set; }

        public int ReceiverItemTemplateId { get; set; }
        public int ReceiverItemAmount { get; set; }

        public TradeOfferStatus Status { get; set; } = TradeOfferStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
