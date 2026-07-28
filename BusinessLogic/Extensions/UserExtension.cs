using DataAccess.Data.Entities;
using DataAccess.Enums;
using System.Net;

namespace BusinessLogic.Extensions;

public static class UserExtension
{
    private static bool IsGameDeveloper(this User user, int gameId)
    {
        return user.DevelopedGames?.Any(g => g.Id == gameId) ?? false;
    }

    private static bool IsModerator(this User user)
    {
        return user.UserRole == UserRole.Moderator;
    }

    private static bool IsDeveloper(this User user)
    {
        return user.UserRole == UserRole.Developer;
    }

    private static bool CanManageGame(this User user, int gameId)
    {
        return user.IsModerator() || (user.IsGameDeveloper(gameId) && user.IsDeveloper());
    }

    public static User EnsureExists(this User? user, string userId)
    {
        if (user == null)
            throw new HttpException($"User with ID {userId} not found", HttpStatusCode.NotFound);

        return user;
    }
    public static User EnsureDeveloper(this User user)
    {
        if (!user.IsDeveloper())
            throw new HttpException("Only developers can perform this action", HttpStatusCode.Forbidden);

        return user;
    }
    public static User EnsureModerator(this User user)
    {
        if (!user.IsModerator())
            throw new HttpException("Only moderator can perform this action", HttpStatusCode.Forbidden);

        return user;
    }
    public static User EnsureHasAccessToGame(this User user, int gameId)
    {
        if (!user.CanManageGame(gameId))
            throw new HttpException("You do not have permission to manage this game", HttpStatusCode.Forbidden);

        return user;
    }
}