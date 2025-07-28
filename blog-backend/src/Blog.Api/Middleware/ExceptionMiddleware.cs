using System.Net;
using System.Text.Json;

namespace Blog.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env; 

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env) 
        {
            _next = next;
            _logger = logger;
            _env = env; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Pass to next middleware or controller
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = _env.IsDevelopment()
                    ? new Dictionary<string, string>
                    {
                        { "message", "An unexpected error occurred." },
                        { "details", ex.Message }
                    }
                    : new Dictionary<string, string>
                    {
                        { "message", "An unexpected error occurred." }
                    };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
