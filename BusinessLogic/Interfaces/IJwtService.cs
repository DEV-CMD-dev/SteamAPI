using BusinessLogic.Configurations;
using DataAccess.Data.Entities;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        IEnumerable<Claim> GetClaims(User user);
        JWT GenerateToken(IEnumerable<Claim> claims);
    }
}
