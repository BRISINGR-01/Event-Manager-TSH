using Logic.Utilities;
using Web.Middlewares.Errors;

namespace Web.Middlewares
{
    public class IpHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;
        private readonly IpHandler ipHandler;

        public IpHandlerMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            ipHandler = new IpHandler();
        }
        public async Task Invoke(HttpContext context)
        {
            var ip = context.Request.HttpContext.Connection.RemoteIpAddress?.MapToIPv6().ToString();

            if (ip == null || ipHandler.IsBanned(ip))
            {
                context.Response.StatusCode = 429;
                return;
            }

            if (ipHandler.CheckIp(ip))
            {
                await _next(context);
            }
            else
            {
                throw new Exception("An ip: " + ip + " tried to access too many times at " + DateTime.Now.ToLongDateString());
            }
        }
    }
}
