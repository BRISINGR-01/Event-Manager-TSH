using Logic.Interfaces;
using Logic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Enums;
using Shared.Errors;
using Web.Middlewares.Authentication;

namespace Web.Pages
{

    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class PageModelWrapper : PageModel
    {
        public IAuthenticationContext Ctx { get; private set; }
        public IManager Manager { get; private set; }
        public PageModelWrapper(IManager manager, IAuthenticationContext ctx, UserRole role = UserRole.Guest) : base()
        {
            Ctx = ctx;
            Manager = manager;
            Utilities.CheckAuthorization(Ctx, role);
        }
        public string? Error { get; set; }
        public IActionResult RedirectToLogIn => RedirectToPage(Utilities.LoginUrl);

        protected IActionResult HandleError(Result res)
        {
            if (res.Exception is AccessDeniedException)
            {
                return RedirectToPage(Utilities.AccessDeniedUrl);
            }
            else if (res.Exception is ServerException)
            {
                return RedirectToPage(Utilities.ServerErrorUrl);
            }

            Error = res.Exception.Message;
            return Page();
        }
        protected IActionResult HandleError(string error)
        {
            Error = error;
            return Page();
        }
        public bool IsPage(string page) => Utilities.IsPage(page, HttpContext);
    }
}
