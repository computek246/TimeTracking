using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace TimeTracking.Web.Helper.Middlewares
{
    /// <summary>
    /// tries to measure request processing time
    /// </summary>
    public class ResponseTimeMiddleware
    {
        // Name of the Response Header, Custom Headers starts with "X-"  
        private const string ResponseHeaderResponseTime = "X-Response-Time-ms";

        // Handle to the next Middleware in the pipeline  
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseTimeMiddleware> _logger;

        public ResponseTimeMiddleware(RequestDelegate next, ILogger<ResponseTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context)
        {
            // skipping measurement of non-actual work like OPTIONS
            if (context.Request.Method == "OPTIONS")
                return _next(context);

            // Start the Timer using Stopwatch  
            var watch = new Stopwatch();
            watch.Start();

            context.Response.OnStarting(() =>
            {
                // Stop the timer information and calculate the time   
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                // Add the Response time information in the Response headers.
                context.Response.Headers[ResponseHeaderResponseTime] = responseTimeForCompleteRequest.ToString();
                var fullUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
                
                _logger.LogTrace($"[Performance] [{context.Request.Method}] Request to {fullUrl} took {responseTimeForCompleteRequest} Milliseconds");
                _logger.LogTrace($"[StatusCode: {ReasonPhrases.GetReasonPhrase(context.Response.StatusCode)}]");
                _logger.LogDebug($"[IpAddress: \"{context.Connection.RemoteIpAddress}\"]");

                return Task.CompletedTask;
            });

            // Call the next delegate/middleware in the pipeline   
            return _next(context);
        }
    }
}
