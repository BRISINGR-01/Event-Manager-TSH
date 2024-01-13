using Logic.Interfaces;
using Logic.Models.Events;
using Logic.Models.Images;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Web.Middlewares.Authentication;
using Web.ViewModels;

namespace Web.Pages.Events
{
    public class EditModel : PageModelWrapper
    {
        public EditModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx, UserRole.EventOrganizer) { }
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }
        [BindProperty]
        public EventViewModel EventViewModel { get; set; } = new();

        public Event? Event { get; set; }
        public IActionResult OnGet()
        {
            if (!Guid.TryParse(Id, out var id)) return RedirectToPage(Utilities.HomeUrl);

            var res = Manager.Event.GetBy(id);

            if (res.IsUnSuccessful) return RedirectToPage(Utilities.HomeUrl);

            Event = res.Value;

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            if (!Guid.TryParse(Id, out var id)) return RedirectToPage(Utilities.HomeUrl);

            if (EventViewModel.Image != null)
            {
                var imageRes = Manager.Image.Create(Image.New(id, Ctx.User.BranchId, EventViewModel.Image.FileName, EventViewModel.Image.OpenReadStream(), ImageType.Background));
                if (imageRes.IsUnSuccessful) return HandleError(imageRes);
            }

            Event @event = new(id, Ctx.User.BranchId, EventViewModel.Title, EventViewModel.Description, EventViewModel.Venue, false);

            if (EventViewModel.Start != null)
            {
                @event = TimedEvent.New(
                    @event,
                    (DateTime)EventViewModel.Start,
                    EventViewModel.End
                );
                if (EventViewModel.Price != null)
                {
                    @event = PaidEvent.New(
                        (TimedEvent)@event,
                        (double)EventViewModel.Price,
                        EventViewModel.MaxParticipants
                    );
                }
            }

            var res = Manager.Event.Update(@event);
            if (res.IsUnSuccessful) return HandleError(res);

            return RedirectToPage(Utilities.HomeUrl);
        }

        public IActionResult OnPostDelete()
        {
            if (Guid.TryParse(Id, out var guid))
            {
                var res = Manager.Event.Delete(guid);
                if (res.IsUnSuccessful) return HandleError(res);
            }

            return RedirectToPage(Utilities.HomeUrl);
        }
    }
}

