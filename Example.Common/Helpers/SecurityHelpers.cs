#region

using System.Text;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace Example.Common.Helpers
{
    public static class SecurityHelpers
    {
        public static SecurityKey CreateSecurityKey(this string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }

        public static SigningCredentials CreateSigningCredentials(this SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}