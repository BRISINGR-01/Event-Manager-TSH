using Logic.Interfaces;
using Logic.Models;
using Logic.Models.Images;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Web.Middlewares.Authentication;
using Web.ViewModels;

namespace Web.Pages.Authentication
{
    public class ProfileModel : PageModelWrapper
    {
        public ProfileModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
        [BindProperty]
        public UserViewModel UserViewModel { get; set; } = new();
        public User? ExpandedLoggedUser { get; private set; }
        public string Email = string.Empty;
        public IActionResult OnGet()
        {
            var res = Manager.User.GetById(Ctx.User.Id);

            if (res.IsUnSuccessful) return HandleError(res.Plain);

            ExpandedLoggedUser = res.Value;

            var cRes = Manager.Credentials.GetById(Ctx.User.Id);
            if (cRes.IsSuccessful) Email = cRes.Value.Email;
            return Page();
        }

        public IActionResult OnPostImage()
        {
            if (UserViewModel.Image == null || !ModelState.IsValid) return RedirectToPage("/Pages/Authentication/Profile");

            var res = Manager.Image.Create(new Image(Ctx.User.Id, Ctx.User.BranchId, UserViewModel.Image.FileName, UserViewModel.Image.OpenReadStream(), ImageType.User));

            if (res.IsUnSuccessful) return HandleError(res);

            return RedirectToPage("/Pages/Authentication/Profile");
        }
    }
}
