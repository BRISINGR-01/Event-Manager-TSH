using Logic.Configuration;
using Logic.Interfaces;
using Logic.Models;
using Microsoft.Extensions.Options;

namespace Web.Middlewares.Authentication
{
    public interface IAuthenticationContext
    {
        bool IsAuthenticated { get; }
        IdentityUser User { get; }
        void SetUser(HttpContext ctx, IManager manager, IOptions<HashingConfig> config);
        public bool ShouldRedirect(HttpContext context);
    }
}