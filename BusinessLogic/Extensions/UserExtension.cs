using DataAccess.Data.Entities;
using DataAccess.Enums;
using System.Net;

namespace BusinessLogic.Extensions;

public static class UserExtension
{
    public static User UserExists(this User? user, string userId)
    {
        if (user == null)
            throw new HttpException($"User with ID {userId} not found", HttpStatusCode.NotFound);

        return user; 
    }
    public static User VerifyCanManageGame(this User user, int gameId)
    {
        if (!user.CanManageGame(gameId))
            throw new HttpException($"User is not allowed to manage achievement", HttpStatusCode.Forbidden);

        return user;
    }
    public static User IsUserDeveloper(this User user)
    {
        if (user.UserRole != UserRole.Developer)
            throw new HttpException("Only developers can create games", HttpStatusCode.Forbidden);

        return user;
    }
    public static bool IsGameDeveloper(this User user, int gameId)
    {
        return user?.DevelopedGames?.Any(g => g.Id == gameId) ?? false;
    }

    public static bool IsModerator(this User user)
    {
        return user?.UserRole == UserRole.Moderator;
    }

    public static bool CanManageGame(this User user, int gameId)
    {
        return user.IsModerator() || user.IsGameDeveloper(gameId);
    }
}