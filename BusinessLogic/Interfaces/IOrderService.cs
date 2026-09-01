using BusinessLogic.DTOs.Order;

namespace BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetOrders(string userId);
        Task Checkout(string userId);
    }
}
