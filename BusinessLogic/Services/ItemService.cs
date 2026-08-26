using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Configurations;
using BusinessLogic.DTOs.Item;
using BusinessLogic.Extensions;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;


namespace BusinessLogic.Services
{
    public class ItemService : IItemService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;
        private readonly FrontendOptions _frontendOptions;

        public ItemService(
            SteamDbContext context,
            IMapper mapper,
            IOptions<FrontendOptions> frontendOptions)
        {
            _context = context;
            _mapper = mapper;
            _frontendOptions = frontendOptions.Value;
        }

        public async Task<PaginatedList<ItemDto>> GetAll(int pageNumber, int pageSize)
        {
            var query = _context.Items
                .AsNoTracking()
                .OrderBy(i => i.Id)
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider);
            return await PaginatedList<ItemDto>.CreateAsync(query, pageNumber, pageSize, _frontendOptions.MaxPaginationPageSize);
        }

        public async Task<ItemDto> GetById(int id)
        {
            var item = await _context.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
                throw new HttpException($"Item with ID {id} not found", HttpStatusCode.NotFound);

            return _mapper.Map<ItemDto>(item);
        }

        public async Task Create(string userId, CreateItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new HttpException("Item name can not be empty", HttpStatusCode.BadRequest);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(dto.GameId);

            var newItem = _mapper.Map<Item>(dto);
            _context.Items.Add(newItem);
            await _context.SaveChangesAsync();
        }

        public async Task Patch(string userId, int id, PatchItemDto dto)
        {
            var item = await GetItemForUpdate(userId, id);

            if (dto.Name != null)
                item.Name = dto.Name;

            if (dto.ImageUrl != null)
                item.ImageUrl = dto.ImageUrl;

            if (dto.Description != null)
                item.Description = dto.Description;

            if (dto.IsTradable != null)
                item.IsTradable = dto.IsTradable.Value;

            await _context.SaveChangesAsync();
        }

        public async Task Put(string userId, int id, PutItemDto dto)
        {
            var item = await GetItemForUpdate(userId, id);

            _mapper.Map(dto, item);

            await _context.SaveChangesAsync();
        }

        public async Task Delete(string userId, int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                throw new HttpException($"Item with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(item.GameId);

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        private async Task<Item> GetItemForUpdate(string userId, int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                throw new HttpException($"Item with ID {id} not found", HttpStatusCode.NotFound);

            var user = await _context.Users
                .Include(u => u.DevelopedGames)
                .FirstOrDefaultAsync(u => u.Id == userId);
            user.EnsureExists(userId).EnsureHasAccessToGame(item.GameId);

            return item;
        }
    }
}
