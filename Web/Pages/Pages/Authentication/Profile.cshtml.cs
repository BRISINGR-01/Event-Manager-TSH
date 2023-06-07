using Domain.Managers;
using Infrastructure.DatabaseManagers;
using Logic;
using Logic.Models;
using Logic.Models.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Enums;
using System.Runtime.CompilerServices;
using Web.ViewModels;

namespace Web.Pages.Authentication
{
    [Authorize]
    public class ProfileModel : PageModelWrapper
    {
        [BindProperty]
        public UserViewModel UserViewModel { get; set; } = new();
        public User? LoggedUser { get; private set; }
        public IActionResult OnGet()
        {
            if (!Initiate()) return NotInitiated;

            var res = Infrastructure.Manager.UserGet(User.Id);

            if (res.IsSuccessful)
            {
                if (res.Value == null)
                {
                    return NotInitiated;
                } else
                {
                    LoggedUser = res.Value;
                }
                return Page();
            } else
            {
                return HandleError(res.Plain);
            }
        }

        public IActionResult OnPostImage()
        {
            if (UserViewModel.Image == null || !ModelState.IsValid) return RedirectToPage("/Pages/Authentication/Profile");
            if (!Initiate()) return NotInitiated;

            var res = Manager.Image.Create(new Image(User.Id, UserViewModel.Image.FileName, UserViewModel.Image.OpenReadStream(), ImageType.User));

            if (!res.IsSuccessful) return HandleError(res);
     
            return RedirectToPage("/Pages/Authentication/Profile");
        }
    }
}
