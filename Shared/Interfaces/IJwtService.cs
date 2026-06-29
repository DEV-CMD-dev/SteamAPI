using System.Security.Claims;
using Shared.Entities;

namespace Shared.Interfaces;

public interface IJwtService
{
    Task<IEnumerable<Claim>> GetClaimsAsync(User user);
    Task<string> GenerateTokenAsync(IEnumerable<Claim> claims);
}