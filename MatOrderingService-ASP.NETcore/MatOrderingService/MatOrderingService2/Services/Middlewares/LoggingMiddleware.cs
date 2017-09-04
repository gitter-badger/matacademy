using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MatOrderingService2.Services.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly EnvironmentOptions _options;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, IOptions<EnvironmentOptions> optionsAccessor)
        {
            _next = next;
            _logger = logger;
            _options = optionsAccessor.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation(_options.WelcomeMessage);
            await this._next(context);
            _logger.LogInformation("12324564");
        }
    }
}
