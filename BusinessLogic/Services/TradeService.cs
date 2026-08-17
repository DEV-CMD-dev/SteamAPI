using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Item;
using BusinessLogic.DTOs.TradeOffer;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;

namespace BusinessLogic.Services
{
    public class TradeService : ITradeService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public TradeService(
            SteamDbContext context,
            IMapper mapper,
            IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task<PaginatedList<TradeOfferDto>> GetTradeOffers(string userId, int pageNumber, int pageSize)
        {
            var query = _context.TradeOffers
                .AsNoTracking()
                .Where(to => to.SenderId == userId || to.ReceiverId == userId)
                .OrderBy(to => to.CreatedAt)
                .ProjectTo<TradeOfferDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<TradeOfferDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task CreateTradeOfferAsync(string senderId, CreateTradeOfferDto dto)
        {
            if (senderId == dto.ReceiverId)
                throw new HttpException("You cannot send a trade offer to yourself", HttpStatusCode.BadRequest);

            var senderInventoryItem = await GetUserItemAsync(senderId, dto.SenderInventoryItemId);
            if (senderInventoryItem == null)
                throw new HttpException("Your inventory item not found", HttpStatusCode.NotFound);

            var receiverInventoryItem = await GetUserItemAsync(dto.ReceiverId, dto.ReceiverInventoryItemId);
            if (receiverInventoryItem == null)
                throw new HttpException("Receiver's inventory item not found", HttpStatusCode.NotFound);

            var offer = new TradeOffer
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                SenderInventoryItemId = dto.SenderInventoryItemId,
                ReceiverInventoryItemId = dto.ReceiverInventoryItemId,
                Status = TradeOfferStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.TradeOffers.Add(offer);
            await _context.SaveChangesAsync();
        }

        public async Task AcceptTradeOfferAsync(string userId, int tradeOfferId)
        {
            var tradeOffer = await GetTradeOffer(userId, tradeOfferId);

            if (tradeOffer.ReceiverId != userId)
                throw new HttpException("You are not authorized to accept this trade offer", HttpStatusCode.Forbidden);

            var senderItem = await _context.InventoryItems.FindAsync(tradeOffer.SenderInventoryItemId);
            var receiverItem = await _context.InventoryItems.FindAsync(tradeOffer.ReceiverInventoryItemId);

            bool isSenderItemValid = senderItem != null && senderItem.UserId == tradeOffer.SenderId;
            bool isReceiverItemValid = receiverItem != null && receiverItem.UserId == tradeOffer.ReceiverId;

            if (!isSenderItemValid || !isReceiverItemValid)
            {
                tradeOffer.Status = TradeOfferStatus.Canceled;
                await _context.SaveChangesAsync();

                throw new HttpException("One of the items is no longer available. Trade canceled.", HttpStatusCode.BadRequest);
            }

            senderItem!.UserId = tradeOffer.ReceiverId;
            receiverItem!.UserId = tradeOffer.SenderId;
            tradeOffer.Status = TradeOfferStatus.Accepted;

            await _context.SaveChangesAsync();
        }

        public async Task CancelTradeOfferAsync(string userId, int tradeOfferId)
        {
            var tradeOffer = await GetTradeOffer(userId, tradeOfferId);

            if (tradeOffer.ReceiverId == userId){
                tradeOffer.Status = TradeOfferStatus.Declined;
            }
            else{
                tradeOffer.Status = TradeOfferStatus.Canceled;
            }

            await _context.SaveChangesAsync();
        }

        private async Task<InventoryItem?> GetUserItemAsync(string userId, int itemId)
        {
            return await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.UserId == userId && i.Id == itemId);
        }

        private async Task<TradeOffer> GetTradeOffer(string userId, int tradeOfferId)
        {
            var tradeOffer = await _context.TradeOffers
                .FirstOrDefaultAsync(to => to.Id == tradeOfferId);

            if (tradeOffer == null || tradeOffer.Status != TradeOfferStatus.Pending)
                throw new HttpException("Trade offer not found or not pending", HttpStatusCode.NotFound);

            if (tradeOffer.ReceiverId != userId && tradeOffer.SenderId != userId)
                throw new HttpException("You are not authorized to cancel this trade offer", HttpStatusCode.Forbidden);

            return tradeOffer;
        }


    }
}
