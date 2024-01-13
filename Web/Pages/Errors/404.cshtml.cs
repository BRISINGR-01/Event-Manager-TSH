using Logic.Interfaces;
using Web.Middlewares.Authentication;

namespace Web.Pages.Errors
{
    public class _404Model : PageModelWrapper
    {
        public _404Model(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
    }
}
