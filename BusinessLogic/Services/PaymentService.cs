using BusinessLogic.Extensions;
using BusinessLogic.Interfaces;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly SteamDbContext _context;

        public PaymentService(SteamDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetBalance(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId);
            return user.WalletBalance;
        }

        public async Task AddFunds(string userId, decimal amount)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId);
            user.WalletBalance += amount;
            await _context.SaveChangesAsync();
        }

        public async Task DeductFunds(string userId, decimal amount)
        {
            var user = await _context.Users.FindAsync(userId);
            user.EnsureExists(userId);
            if (user.WalletBalance < amount)
                throw new HttpException("Insufficient funds", System.Net.HttpStatusCode.BadRequest);
            user.WalletBalance -= amount;
            await _context.SaveChangesAsync();
        }

    }
}
