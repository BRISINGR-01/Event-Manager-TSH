using Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Web.Middlewares.Authentication;

namespace Web.Pages.Authentication
{
    [AllowAnonymous]
    public class AccessDeniedModel : PageModelWrapper
    {
        public AccessDeniedModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
    }
}
