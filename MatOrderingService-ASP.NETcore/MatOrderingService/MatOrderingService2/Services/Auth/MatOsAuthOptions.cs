using Microsoft.AspNetCore.Builder;

namespace MatOrderingService2.Services.Auth
{
    public class MatOsAuthOptions : AuthenticationOptions
    {
        public string SecurityKey { get; set; }
    }
}
