using Logic.Configuration;
using Logic.Interfaces;
using Logic.Models;
using Logic.Utilities;
using Microsoft.Extensions.Options;
using Shared;

namespace Web.Middlewares.Authentication
{
    public class AuthenticationContext : IAuthenticationContext
    {
        public bool IsAuthenticated => _user != null;
        private IdentityUser? _user;
        public IdentityUser User => _user ?? throw new Exception("Not Authenticated");
        public void SetUser(HttpContext ctx, IManager manager, IOptions<HashingConfig> config)
        {
            _user = null;

            var cookie = ctx.Request.Cookies[Constants.TokenCookie];
            if (cookie == null) return;

            var token = Encryption.Decrypt(cookie);
            if (string.IsNullOrEmpty(token) || token.Split(";").Length != 2) return;

            var email = token.Split(";")[0];
            var password = token.Split(";")[1];

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return;

            var res = manager.Credentials.GetByEmail(email);

            if (res.IsUnSuccessful) return;

            var credentials = res.Value;
            credentials.Configure(config.Value);

            if (!credentials.VerifyPassword(password)) return;

            var userRes = manager.User.GetById(credentials.Id);

            _user = userRes.Value;
        }
        public bool ShouldRedirect(HttpContext context)
        {
            return !IsAuthenticated
                && context.Request.Path.ToString().StartsWith("/Pages")
                && context.Request.Path != "/Pages/Authentication/LogIn";
        }
    }
}
