using DataAccess.Data.Entities;
using DataAccess.Enums;

namespace BusinessLogic.Extensions;

public static class UserExtension
{
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