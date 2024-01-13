using Logic.Interfaces;
using Logic.Models;
using Newtonsoft.Json;
using Shared.Enums;
using Shared.Errors;
using Web.Middlewares.Authentication;

namespace Web
{
    public static class Utilities
    {
        public static string LoginUrl => "/Pages/Authentication/LogIn";
        public static string AccessDeniedUrl => "/Pages/Authentication/AccessDenied";
        public static string ServerErrorUrl => "/Errors/ServerError";
        public static string HomeUrl => "/Index";
        public static void CheckAuthorization(IAuthenticationContext ctx, UserRole role)
        {
            if (!ctx.IsAuthenticated)
            {
                if (UserRole.Guest == role) return;

                throw new AccessDeniedException();
            }

            if ((ctx.User.Role & role) != ctx.User.Role) throw new AccessDeniedException(ctx.User.Id);
        }
        public static bool IsPage(string path, HttpContext ctx) => ctx.Request.Path.ToString().EndsWith(path);
        public static void SubscribeTonotifications(HttpContext ctx, IManager manager)
        {
            try
            {
                using var reader = new StreamReader(ctx.Request.Body);
                var body = reader.ReadToEndAsync().GetAwaiter().GetResult();

                var data = JsonConvert.DeserializeObject<PushNotificationSubscription>(body);

                manager.User.Subscribe(data);
            }
            catch { };
        }
    }
}
