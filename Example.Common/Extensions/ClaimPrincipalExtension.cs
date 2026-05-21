#region

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

#endregion

namespace Example.Common.Extensions
{
    public static class ClaimPrincipalExtension
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        }

        public static List<string> ClaimRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }

        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault();
            return userId != null ? int.Parse(userId) : 0;
        }
    }
}