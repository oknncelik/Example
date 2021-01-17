using System;

namespace Example.Common.Security.Jwt.Models
{
    public class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
