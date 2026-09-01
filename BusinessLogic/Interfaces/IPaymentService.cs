using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Interfaces
{
    public interface IPaymentService
    {
        Task<decimal> GetBalance(string userId);
        Task AddFunds(string userId, decimal amount);
        Task DeductFunds(string userId, decimal amount);
    }
}
