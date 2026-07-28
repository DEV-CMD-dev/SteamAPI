using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Extensions;

public static class UserManagerExtension
{
    public static async Task<User?> FindByIdentifierAsync(this UserManager<User> userManager, string identifier)
    {
        return identifier.Contains('@')
            ? await userManager.FindByEmailAsync(identifier)
            : await userManager.FindByNameAsync(identifier);
    }
}