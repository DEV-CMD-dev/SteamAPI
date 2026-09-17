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
        private readonly IEmailService _emailService;

        public OrderService(SteamDbContext context, IMapper mapper, IPaymentService paymentService, IEmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _paymentService = paymentService;
            _emailService = emailService;
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
                    Title = c.Game.Title,
                    GameId = c.GameId,
                    Price = c.Game.Price,
                    Discount = c.Game.Discount
                })
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
                throw new HttpException($"Any items in the cart not found", HttpStatusCode.NotFound);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var totalPrice = cartItems.Sum(c => c.Price * (1 - c.Discount / 100m));

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

                    var userGames = cartItems.Select(c => new UserGame
                    {
                        UserId = userId,
                        GameId = c.GameId,
                        PurchasedAt = DateTime.UtcNow,
                        LastPlayDate = DateTime.UtcNow, 
                        PlayTimeMinutes = 0,
                        IsInstalled = false
                    }).ToList();
                    _context.UserGames.AddRange(userGames);

                    await _context.SaveChangesAsync();
                    await _context.Carts.Where(c => c.UserId == userId).ExecuteDeleteAsync();

                    await transaction.CommitAsync();

                    try
                    {
                        if (user != null && !string.IsNullOrEmpty(user.Email))
                        {
                           
                            var purchasedItemsForEmail = cartItems.Select(c => new OrderItemDto
                            {
                                Title = c.Title,
                                Price = c.Price * (1 - c.Discount / 100m) 
                            }).ToList();

                  
                            await _emailService.SendOrderReceiptAsync(user.Email, user.UserName, order, purchasedItemsForEmail);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send receipt email to {user?.Email}: {ex.Message}");
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<decimal> GetBalance(string userId)
        {
            var balance = await _paymentService.GetBalance(userId);

            return balance;
        }

    }
}
