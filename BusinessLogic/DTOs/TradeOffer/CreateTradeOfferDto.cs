using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.DTOs.TradeOffer
{
    public class CreateTradeOfferDto
    {
        public string ReceiverId { get; set; }
        public int SenderInventoryItemId { get; set; }
        public int ReceiverInventoryItemId { get; set; }
    }
}
