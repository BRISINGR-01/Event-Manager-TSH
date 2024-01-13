using Shared;
using Shared.Errors;

namespace Web.Middlewares.Errors
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AccessDeniedException ex)
            {
                _logger.Log(LogLevel.Error, ex, "An unhandled exception occurred.");
                context.Response.Redirect("/Pages/Authentication/AccessDenied");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "An unhandled exception occurred.");
                Helpers.ReportException(ex);
                context.Response.StatusCode = 429;
            }
        }
    }
}
