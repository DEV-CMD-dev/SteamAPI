using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.InventoryItem;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
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
    public class InventoryItemService
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
        public async Task<PaginatedList<InventoryItemDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.InventoryItems
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ProjectTo<InventoryItemDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<InventoryItemDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<InventoryItemDto> GetById(int id)
        {
            var inventoryItem = await _context.InventoryItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (inventoryItem == null)
                throw new HttpException($"Inventory item with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<InventoryItemDto>(inventoryItem);
        }

        // User can only get item
        public async Task Create(string userId, CreateInventoryItemDto dto)
        {
            var temp = await _context.Items.FindAsync(dto.ItemId);

            if (temp == null)
                throw new HttpException($"Item with ID {dto.ItemId} not found", HttpStatusCode.NotFound);

            var newInventoryItem = new InventoryItem
            {
                UserId = userId,
                ItemId = dto.ItemId,
                Quantity = 1,
                AcquiredAt = DateTime.UtcNow
            };

            _context.InventoryItems.Add(newInventoryItem);
            await _context.SaveChangesAsync();
        }

        //public async Task Patch(int id, string userId, PatchInventoryItemDto dto)
        //{
        //    if (string.IsNullOrWhiteSpace(dto.UserId) && string.IsNullOrWhiteSpace(dto.ItemId))
        //        throw new HttpException("Inventory item user ID and item ID can not be empty", HttpStatusCode.BadRequest);

        //    await Update(userId, id, dto);
        //}

        //this for trade
        //public async Task Put(int id, string userId, PutInventoryItemDto dto)
        //{
        //    if (string.IsNullOrWhiteSpace(dto.Quantity) || string.IsNullOrEmpty(dto.ItemId))
        //        throw new HttpException("Inventory item user ID or item ID can not be empty", HttpStatusCode.BadRequest);

        //    await Update(userId, id, dto);
        //}

        public async Task Delete(string userId, int id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);

            if (inventoryItem == null)
                throw new HttpException($"Inventory item with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId);

            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();
        }

        private async Task Update<TDto>(string userId, int id, TDto dto)
        {
            var existingInventoryItem = await _context.InventoryItems.FindAsync(id);

            if (existingInventoryItem == null)
                throw new HttpException($"Inventory item with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users.Include(u => u.DevelopedGames).FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId);

            _mapper.Map(dto, existingInventoryItem);

            await _context.SaveChangesAsync();
        }
    }
}
