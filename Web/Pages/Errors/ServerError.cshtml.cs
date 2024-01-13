using Logic.Interfaces;
using Web.Middlewares.Authentication;

namespace Web.Pages.Errors
{
    public class ServerErrorModel : PageModelWrapper
    {
        public ServerErrorModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
    }
}
