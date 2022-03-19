using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeHandlingMiddleware> _logger;
        private readonly Stopwatch _stopwatch;
        public RequestTimeHandlingMiddleware(ILogger<RequestTimeHandlingMiddleware> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var elapsedMiliseconds = _stopwatch.ElapsedMilliseconds;
            if(elapsedMiliseconds / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMiliseconds} ms";
                _logger.LogInformation(message);
            }
        }
    }
}
