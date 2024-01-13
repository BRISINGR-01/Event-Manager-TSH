using Logic.Configuration;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Middlewares.Authentication;

namespace Web.Pages
{
    public class IndexModel : PageModelWrapper
    {
        private readonly VapidConfig vapid;
        public IndexModel(IOptions<VapidConfig> vapid, IManager manager, IAuthenticationContext ctx) : base(manager, ctx)
        {
            this.vapid = vapid.Value;
        }
        public IActionResult OnGet()
        {
            ViewData["applicationServerKey"] = vapid.PublicKey;
            return Ctx.IsAuthenticated ? Page() : RedirectToLogIn;
        }
    }
}