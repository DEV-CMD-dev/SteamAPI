using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.InventoryItem;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLogic.Services
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public InventoryItemService(
           SteamDbContext context,
           IMapper mapper,
           IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }
        public async Task<PaginatedList<InventoryItemDto>> GetAll(string userId, int pageNumber, int pageSize)
        {
            var query = _context.InventoryItems
                .AsNoTracking()
                .Where(i => i.UserId == userId)
                .OrderBy(t => t.Id)
                .ProjectTo<InventoryItemDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<InventoryItemDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task BuyFromStoreAsync(string userId, int itemId)
        {
            var itemTemplate = await _context.Items.FindAsync(itemId);
            if (itemTemplate == null)
                throw new HttpException($"Item with ID {itemId} not found", HttpStatusCode.NotFound);

            // TODO: add payment checking logic here 

            var newInventoryItem = new InventoryItem
            {
                UserId = userId,
                ItemId = itemId,      
                Quantity = 1,
                AcquiredAt = DateTime.UtcNow,
            };

            _context.InventoryItems.Add(newInventoryItem);
            await _context.SaveChangesAsync();
        }
        public async Task SellFromInventoryAsync(string userId, int inventoryItemId)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(inventoryItemId);

            if (inventoryItem == null)
                throw new HttpException($"Inventory item with ID {inventoryItemId} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(inventoryItem.Item.GameId);

            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();
        }
    }
}
