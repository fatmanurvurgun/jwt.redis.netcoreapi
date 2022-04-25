using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jwt.redis.netcoreapi.Filters
{
    public class ExcepitonHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExcepitonHandlerMiddleware> _logger;

        public ExcepitonHandlerMiddleware(RequestDelegate next, ILogger<ExcepitonHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
        }
    }
}
