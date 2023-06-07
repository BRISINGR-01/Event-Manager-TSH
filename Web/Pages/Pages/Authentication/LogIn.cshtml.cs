using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Shared;
using Microsoft.AspNetCore.Authorization;
using Logic;
using Web.ViewModels;
using Infrastructure;
using Logic.Models;

namespace Web.Pages.Authentication
{
    [AllowAnonymous]
    public class LogInModel : PageModelWrapper
    {
        public bool AreCredentialsWrong = false;
        [BindProperty]
        public UserViewModel LoggedUser { get; set; } = new();
        [BindProperty]
        public bool RememberMe { get; set; }
        public IActionResult OnGet(string? returnUrl = null)
        { 
            if (!Initiate()) return Page();

            var res = Manager.UserGet(User.Id);
            if (!res.IsSuccessful || res.Value == null) return Page();

            LogUserIn(res.Value);

            returnUrl ??= Url.Content("~/");
            return RedirectToPage(string.IsNullOrEmpty(returnUrl) || returnUrl == "/" ? "/Pages/Events/List" : returnUrl);
        }

        private IActionResult UnsuccessfulLogIn { get
            {
                AreCredentialsWrong = true;
                return Page();
            }
        }

        public IActionResult OnPostLogIn(string? returnUrl = null)
        {
            if (!ModelState.IsValid) return UnsuccessfulLogIn;
            Initiate();

            var res = Manager.CheckCredentials(LoggedUser.Email, LoggedUser.Password);
            if (!res.IsSuccessful || res.Value == null) return UnsuccessfulLogIn;

            User user = res.Value;

            LogUserIn(user);

            if (RememberMe)
            {
                CookieOptions cOption = new()
                {
                    Expires = DateTime.Now.AddYears(2)
                };
                var emailCookie = Manager.Encrypt(user.Email);
                var passwordCookie = Manager.Encrypt(user.Password);
                
                if (emailCookie != null) Response.Cookies.Append(Constants.EmailCookie, emailCookie, cOption);
                if (passwordCookie != null) Response.Cookies.Append(Constants.PasswordCookie, passwordCookie, cOption);
            }

            returnUrl ??= Url.Content("~/");
            
            return RedirectToPage(string.IsNullOrEmpty(returnUrl) || returnUrl == "/" ? "/Pages/Events/List" : returnUrl);
        }
    }
}
