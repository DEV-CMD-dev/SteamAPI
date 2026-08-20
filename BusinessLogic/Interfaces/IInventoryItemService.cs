using BusinessLogic.DTOs.Achievement;
using BusinessLogic.DTOs.InventoryItem;
using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IInventoryItemService
    {
        Task<PaginatedList<InventoryItemDto>> GetAll(string userId, int pageNumber, int pageSize);
        Task BuyFromStoreAsync(string userId, int itemId);
        Task SellFromInventoryAsync(string userId, int inventoryItemId);
    }
}
