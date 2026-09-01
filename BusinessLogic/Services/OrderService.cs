using AutoMapper;
using BusinessLogic.DTOs.Order;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Data.Entities;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly SteamDbContext _context;
        private readonly IMapper _mapper;

        private readonly IPaymentService _paymentService;
        public OrderService(SteamDbContext context, IMapper mapper, IPaymentService paymentService)
        {
            _context = context;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<List<OrderDto>> GetOrders(string userId)
        {
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(g => g.Game)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    TotalAmount = o.TotalAmount,
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        Title = oi.Game.Title,
                        GameId = oi.GameId,
                        Price = oi.Price
                    }).ToList()
                })
                .ToListAsync();
            return orders;
        }

        public async Task Checkout(string userId)
        {
            var cartItems = await _context.Carts
                .Where(u => u.UserId == userId)
                .Select(c => new CartDto
                {
                    GameId = c.GameId,
                    Price = c.Game.Price
                })
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                throw new HttpException($"Any items in the cart not found", HttpStatusCode.NotFound);

            var totalPrice = cartItems.Sum(c => c.Price);

            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _paymentService.DeductFunds(userId, totalPrice);

                    var order = new Order
                    {
                        UserId = userId,
                        TotalAmount = totalPrice,
                        OrderDate = DateTime.UtcNow,
                        OrderItems = cartItems.Select(c => new OrderItem
                        {
                            GameId = c.GameId,
                            Price = c.Price
                        }).ToList()
                    };

                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    await _context.Carts.Where(c => c.UserId == userId).ExecuteDeleteAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
