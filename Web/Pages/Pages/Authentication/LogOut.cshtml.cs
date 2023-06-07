using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared;

namespace Web.Pages.Authentication
{
    public class LogOutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete(Constants.EmailCookie);
            Response.Cookies.Delete(Constants.PasswordCookie);
            HttpContext.Session.Clear();
            return RedirectToPage("/Pages/Authentication/LogIn");
        }
    }
}
