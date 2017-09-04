using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MatOrderingService2.Filters
{
    public class EntityBadRequestExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<EntityBadRequestExceptionFilter> _logger;

        public EntityBadRequestExceptionFilter(ILogger<EntityBadRequestExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)

        {
            if (context.Exception is Exception)
            {
                _logger.LogInformation("Error111111111:" + context.Exception.Message);
                context.Result = new BadRequestResult();
                context.ExceptionHandled = true;
            }
        }
    }
}
