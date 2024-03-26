using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BooksAPI.BE.Util;

public static class UserValidationUtil
{
    public static int IsUserIdFromRequestValidWithAuthUser(HttpContext httpContext, string userId)
    {
        string? userIdFromAuth = GetUserIdFromAuth(httpContext);

        if (userIdFromAuth == null)
        {
            return StatusCodes.Status401Unauthorized;
        }

        if (userId != userIdFromAuth)
        {
            return StatusCodes.Status403Forbidden;
        }

        return StatusCodes.Status100Continue;
    }


    private static string? GetUserIdFromAuth(HttpContext httpContext)
    {
        ClaimsPrincipal user = httpContext.User;
        string? userIdFromAuth = null;
        foreach (Claim userClaim in user.Claims)
        {
            if (userClaim.Type == JwtRegisteredClaimNames.NameId)
            {
                userIdFromAuth = userClaim.Value;
            }
        }

        return userIdFromAuth;
    }
}