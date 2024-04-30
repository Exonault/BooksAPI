using System.Security.Claims;

namespace BooksAPI.FE.Util;

public static class UserUtil
{
    public static string GetUserId(ClaimsPrincipal claimsPrincipal)
    {
        Claim? claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "userId");

        if (claim is not null)
        {
            return claim.Value;
        }
        else return "";

    }
    
}