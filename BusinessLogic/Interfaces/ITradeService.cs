using BusinessLogic.DTOs.TradeOffer;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface ITradeService
    {
        Task<PaginatedList<TradeOfferDto>> GetTradeOffers(string userId, int pageNumber, int pageSize);
        Task CreateTradeOfferAsync(string senderId, CreateTradeOfferDto dto);
        Task AcceptTradeOfferAsync(string userId, int tradeOfferId);
        Task CancelTradeOfferAsync(string userId, int tradeOfferId);
    }
}
