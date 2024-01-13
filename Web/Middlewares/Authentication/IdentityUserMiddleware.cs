using Logic.Configuration;
using Logic.Interfaces;
using Microsoft.Extensions.Options;
using Web.Middlewares.Errors;

namespace Web.Middlewares.Authentication
{
    public class IdentityUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;

        public IdentityUserMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context, IManager manager, IAuthenticationContext authContext, IOptions<HashingConfig> config)
        {
            authContext.SetUser(context, manager, config);
            if (authContext.ShouldRedirect(context))
            {
                context.Response.Headers.Location = "/Pages/Authentication/LogIn";
                context.Response.StatusCode = 302;
            }

            await _next(context);
        }
    }
}
