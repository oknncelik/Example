using Microsoft.OpenApi.Models;

namespace Example.Api.Models
{
    public class SwaggerInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public OpenApiContact Contact { get; set; }
        public AuthInfo Authorization { get; set; }
    }

    public class AuthInfo
    {
        public string AuthenticationScheme { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
