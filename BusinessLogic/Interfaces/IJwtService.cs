using DataAccess.Data.Entities;
using System.Security.Claims;

namespace BusinessLogic.Interfaces
{
    public interface IJwtService
    {
        IEnumerable<Claim> GetClaims(User user);
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
