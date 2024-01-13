using Logic.Interfaces;
using Logic.Models.Events;
using Logic.Models.Images;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Enums;
using Web.Middlewares.Authentication;
using Web.ViewModels;

namespace Web.Pages.Events
{
    public class CreateModel : PageModelWrapper
    {
        public CreateModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx, UserRole.EventOrganizer) { }
        [BindProperty(SupportsGet = true)]
        public string? Suggestion { get; set; }
        [BindProperty]
        public EventViewModel EventViewModel { get; set; } = new();
        public Event? SuggestionData = null;
        public void OnGet()
        {
            if (!Guid.TryParse(Suggestion, out Guid Id)) return;

            var res = Manager.Event.GetById(Id);
            if (res.IsUnSuccessful) return;

            SuggestionData = res.Value;
            if (SuggestionData == null) return;

            EventViewModel.Description = SuggestionData.Description;
        }
        public virtual IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            if (EventViewModel.Image == null)
            {
                Error = "You need to upload an image!";
                return Page();
            }

            Guid id = Helpers.NewGuid;

            var imageRes = Manager.Image.Create(Image.New(
                id,
                Ctx.User.BranchId,
                EventViewModel.Image.FileName,
                EventViewModel.Image.OpenReadStream(),
                ImageType.Background)
            );
            if (imageRes.IsUnSuccessful) return HandleError(imageRes);

            Event @event = Event.New(id, Ctx.User.BranchId, EventViewModel.Title, EventViewModel.Description, EventViewModel.Venue, false);

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

            var res = Manager.Event.Create(@event);
            if (res.IsUnSuccessful) return HandleError(res);

            return RedirectToPage("/Pages/Events/List");
        }
    }
}
