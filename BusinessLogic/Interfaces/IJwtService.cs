using System.Security.Claims;
using Shared.Entities;

namespace BusinessLogic.Interfaces;

public interface IJwtService
{
    Task<IEnumerable<Claim>> GetClaimsAsync(User user);
    Task<string> GenerateTokenAsync(IEnumerable<Claim> claims);
}