using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace LibraryAPI.Application.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        //Logging request that took more than 4 seconds
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Restart();
            await next.Invoke(context);
            _stopwatch.Stop();
            var elapsedMiliseconds = _stopwatch.ElapsedMilliseconds;
            if (elapsedMiliseconds / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMiliseconds}";
                _logger.LogInformation(message);
            }
        }
    }
}
