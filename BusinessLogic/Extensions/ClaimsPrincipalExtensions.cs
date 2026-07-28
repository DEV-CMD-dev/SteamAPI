using System.Net;
using System.Security.Claims;

namespace BusinessLogic.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetRequiredUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new HttpException(
                    "User identity could not be verified.",
                    HttpStatusCode.Unauthorized);

            return userId;
        }
    }
}
