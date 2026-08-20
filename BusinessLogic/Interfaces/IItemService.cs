using BusinessLogic.DTOs.Item;
using BusinessLogic.DTOs.Tag;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IItemService
    {
        Task<PaginatedList<ItemDto>> GetAll(int pageNumber, int pageSize);
        Task<ItemDto> GetById(int id);
        Task Create(string userId, CreateItemDto dto);
        Task Patch(string userId, int id, PatchItemDto dto);
        Task Put(string userId, int id, PutItemDto dto);
        Task Delete(string userId, int id);
    }
}
