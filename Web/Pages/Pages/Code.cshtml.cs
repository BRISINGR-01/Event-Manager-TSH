using Logic.Interfaces;
using Logic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Web.Middlewares.Authentication;

namespace Web.Pages.Pages
{
    public class CodeModel : PageModelWrapper
    {
        public CodeModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
        public string? Code { get; private set; }
        public IActionResult OnGetAsync()
        {
            if (Ctx.User.IsEventOrganizer) return RedirectToPage("/Pages/CheckCode");

            var res = Manager.User.GetById(Ctx.User.Id);

            if (res.IsUnSuccessful) return HandleError(res.Plain);

            Code = Encryption.Encrypt(res.Value.Id.ToString());

            if (Code == null) return HandleError("An error occurred");

            return Page();
        }
    }
}
