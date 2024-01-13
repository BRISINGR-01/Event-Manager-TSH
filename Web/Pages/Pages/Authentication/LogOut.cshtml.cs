using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Web.Middlewares.Authentication;

namespace Web.Pages.Authentication
{
    public class LogOutModel : PageModelWrapper
    {
        public LogOutModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
        public IActionResult OnGet()
        {
            Response.Cookies.Delete(Constants.TokenCookie);
            HttpContext.Session.Clear();
            return RedirectToLogIn;
        }
    }
}
