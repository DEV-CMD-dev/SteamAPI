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

        public async Task CreateTradeOfferAsync(string senderId,CreateTradeOfferDto dto)
        {
            if (senderId == dto.ReceiverId)
                throw new HttpException("You cannot send a trade offer to yourself", HttpStatusCode.BadRequest);

            var senderInventoryItem = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.UserId == senderId && i.Id == dto.SenderInventoryItemId);

            if (senderInventoryItem == null)
                throw new HttpException("Your inventory item not found", HttpStatusCode.NotFound);

            var receiverInventoryItem = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.UserId == dto.ReceiverId && i.Id == dto.ReceiverInventoryItemId);

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

    }
}
