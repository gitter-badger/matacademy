using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MatOrderingService2.Services.Auth
{
    public class MatOsAuthMiddleware : AuthenticationMiddleware<MatOsAuthOptions>
    {
        public MatOsAuthMiddleware(RequestDelegate next, IOptions<MatOsAuthOptions> options, ILoggerFactory loggerFactory, UrlEncoder encoder) :
            base(next, options, loggerFactory, encoder)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (encoder == null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        protected override AuthenticationHandler<MatOsAuthOptions> CreateHandler()
        {
            return new MatOsAuthHandler();
        }
    }
}
