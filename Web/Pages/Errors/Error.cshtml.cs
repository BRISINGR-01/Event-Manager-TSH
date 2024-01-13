using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Middlewares.Authentication;

namespace Web.Pages.Errors
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModelWrapper
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public ErrorModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx)
        {
        }

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}