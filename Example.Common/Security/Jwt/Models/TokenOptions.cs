namespace Example.Common.Security.Jwt.Models
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Expiration { get; set; }
        public string Key { get; set; }
    }
}
