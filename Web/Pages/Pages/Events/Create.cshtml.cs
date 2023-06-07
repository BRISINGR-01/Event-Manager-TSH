using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Web.ViewModels;
using Domain.Managers;
using Infrastructure.DatabaseManagers;
using Logic.Models;
using Shared;
using Shared.Enums;

namespace Web.Pages.Events
{
    [Authorize(Roles = "EventOrganizer")]
    public class CreateModel : PageModelWrapper
    {
        [BindProperty]
        public EventViewModel Event { get; set; } = new();
        public IActionResult OnGet()
        {
            return Initiate() ? Page() : NotInitiated;
        }
        public IActionResult OnPost()
        {
            if (!Initiate()) return NotInitiated;
            if (!ModelState.IsValid) return Page();

            if (Event.Image == null)
            {
                Error = "You need to upload an image!";
                return Page();
            }

            Event.Id = Helpers.NewGuid;
            Event.BranchId = User.BranchId;

            var imageRes = Manager.Image.Create(new Logic.Models.Images.Image(Event.Id, Event.Image.FileName, Event.Image.OpenReadStream(), ImageType.Background));
            if (!imageRes.IsSuccessful) return HandleError(imageRes);

            var res = Manager.Event.Create(Event.Map());
            if (!res.IsSuccessful) return HandleError(res);

            return RedirectToPage("/Pages/Events/List");
        }
    }
}
