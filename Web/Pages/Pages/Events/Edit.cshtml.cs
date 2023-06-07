using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Enums;
using Shared;
using Web.ViewModels;
using Logic.Models.Events;

namespace Web.Pages.Events
{
    public class EditModel : PageModelWrapper
    {
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }
        [BindProperty]
        public EventViewModel EventModel { get; set; } = new();
        public Event? Event { get; set; }
        public IActionResult OnGet()
        {
            if (!Initiate()) return NotInitiated;

            if (!Guid.TryParse(Id, out var id)) return RedirectToPage("/Pages/Events/List");

            var res = Manager.Event.GetBy(id);

            if (!res.IsSuccessful || res.Value == null)
            {
                Error = "Cannot find event";
                return Page();
            }

            Event = res.Value;

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!Initiate()) return NotInitiated;
            if (!ModelState.IsValid) return Page();
            if (!Guid.TryParse(Id, out var id)) return RedirectToPage("/Pages/Events/List");

            EventModel.Id = id;
            EventModel.BranchId = User.BranchId;

            if (EventModel.Image != null)
            {
                var imageRes = Manager.Image.Update(new Logic.Models.Images.Image(EventModel.Id, EventModel.Image.FileName, EventModel.Image.OpenReadStream(), ImageType.Background));
                if (!imageRes.IsSuccessful) return HandleError(imageRes);
            }

            var res = Manager.Event.Update(EventModel.Map());
            if (!res.IsSuccessful) return HandleError(res);

            return RedirectToPage("/Pages/Events/List");
        }

        public IActionResult OnPostDelete()
        {
            if (!Initiate()) return NotInitiated;

            if (!User.IsEventOrganizer) return NotInitiated;

            if (Guid.TryParse(Id, out var guid))
            {
                var res = Manager.Event.Delete(guid);
                if (!res.IsSuccessful) return HandleError(res);
            }

            return RedirectToPage("/Pages/Events/List");
        }
    }
}

