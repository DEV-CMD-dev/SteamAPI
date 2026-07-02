using DataAccess.Data.Entities;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        Task<IEnumerable<Claim>> GetClaimsAsync(User user);
        Task<string> GenerateTokenAsync(IEnumerable<Claim> claims);
    }
}
