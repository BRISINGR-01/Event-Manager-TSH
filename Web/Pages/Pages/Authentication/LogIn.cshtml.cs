using Logic.Configuration;
using Logic.Interfaces;
using Logic.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared;
using Web.Middlewares.Authentication;
using Web.ViewModels;

namespace Web.Pages.Authentication
{
    [AllowAnonymous]
    public class LogInModel : PageModelWrapper
    {
        private HashingConfig Config;
        public bool AreCredentialsWrong = false;
        [BindProperty]
        public new UserViewModel User { get; set; } = new();
        [BindProperty]
        public bool RememberMe { get; set; }
        public LogInModel(IOptions<HashingConfig> config, IManager manager, IAuthenticationContext ctx) : base(manager, ctx)
        {
            Config = config.Value;
        }
        public IActionResult OnGet(string? returnUrl = null)
        {
            if (!Ctx.IsAuthenticated) return Page();

            return RedirectToPage(returnUrl);
        }

        private IActionResult UnsuccessfulLogIn
        {
            get
            {
                AreCredentialsWrong = true;
                return Page();
            }
        }

        public IActionResult OnPostLogIn(string? returnUrl = null)
        {
            if (!ModelState.IsValid) return UnsuccessfulLogIn;

            var res = Manager.Credentials.GetByEmail(User.Email);
            if (res.IsUnSuccessful) return UnsuccessfulLogIn;

            var credentials = res.Value;
            credentials.Configure(Config);

            if (!credentials.VerifyPassword(User.Password)) HandleError("Incorrect credentials!");

            if (RememberMe)
            {
                CookieOptions cOption = new()
                {
                    Expires = DateTime.Now.AddYears(2)
                };
                var encryptedToken = Encryption.Encrypt(credentials.Email + ";" + credentials.PasswordHash);

                if (encryptedToken != null)
                {
                    Response.Cookies.Append(Constants.TokenCookie, encryptedToken, cOption);
                }
            }

            returnUrl ??= Url.Content("~/");

            return RedirectToPage(returnUrl);
        }
    }
}
