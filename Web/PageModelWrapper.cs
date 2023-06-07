using Infrastructure;
using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Enums;
using Shared.Errors;
using System.Configuration;
using System.Net;
using System.Security.Claims;
using Org.BouncyCastle.Bcpg;
using Shared;

namespace Web
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class PageModelWrapper : PageModel
    {
        private static Exception InitiateException { get => new("You need to call `InitiateUser` first!"); }
        
        private IdentityUser? _user;
        new public IdentityUser User { get => _user ?? throw InitiateException; }
        private Manager? _manager;
        protected Manager Manager { get => _manager ?? throw InitiateException; }
        private string? redirection;
        protected IActionResult NotInitiated { get => redirection != null ? RedirectToPage(redirection) : this.GetType().Name == "LogInModel" ? throw new Exception("You must not return NotInitiated, but Page() in the LogInModel") : RedirectToPage("/Pages/Authentication/LogIn"); }

        [BindProperty]
        public string? Error { get; set; }
        protected IActionResult HandleError(Result res)
        {
            if (res.Redirection != null) return RedirectToPage(res.Redirection);

            Error = res.Error;
            return Page();
        }
        protected IActionResult HandleError(string Error)
        {
            this.Error = Error;
            return Page();
        }
        protected bool Initiate()
        {
            if (!GetUserFromClaims())
            {
                GetUserFromCookies();
                if (redirection != null) return false;
            }

            try
            {
                if (_user != null) _manager = new(_user);
            }
            catch
            {
                redirection = "/Errors/ServerError";
                return false;
            }

            return _user != null;
        }
        private bool GetUserFromClaims()
        {
            ClaimsPrincipal claimsPrincipal = base.User;

            if (claimsPrincipal.Identity?.IsAuthenticated != true) return false;

            Claim? id = claimsPrincipal.FindFirst("id");
            Claim? branchId = claimsPrincipal.FindFirst("branchId");
            Claim? role = claimsPrincipal.FindFirst(ClaimTypes.Role);

            if (id == null || branchId == null || role == null) return false;

            if (
                !Guid.TryParse(id.Value, out var guid) ||
                !Guid.TryParse(branchId.Value, out var branchGuid) ||
                !Enum.TryParse<UserRole>(role.Value, out var userRole)
            ) return false;

            _user = new IdentityUser(guid, branchGuid, userRole);

            return true;
        }
        private bool GetUserFromCookies()
        {
            var email = Manager.Decrypt(Request.Cookies[Constants.EmailCookie]);
            var password = Manager.Decrypt(Request.Cookies[Constants.PasswordCookie]);

            if (email == null || password == null) return false;

            var res = Manager.CheckCredentials(email, password);

            if (res.Redirection != null)
            {
                redirection = res.Redirection;
                return false;
            }
            if (!res.IsSuccessful || res.Value == null) return false;
                
            LogUserIn(res.Value);

            return true;
        }

        protected void LogUserIn(IdentityUser user)
        {
            List<Claim> claims = new()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("branchId", user.BranchId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));
        }
    }
}
