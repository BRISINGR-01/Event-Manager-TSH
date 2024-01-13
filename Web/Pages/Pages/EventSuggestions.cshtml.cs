using Logic.Interfaces;
using Logic.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Web.Middlewares.Authentication;
using Web.ViewModels;

namespace Web.Pages.Pages
{
    public class EventSuggestionsModel : PageModelWrapper
    {
        public EventSuggestionsModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx, UserRole.Student) { }
        [BindProperty]
        public List<Event> Suggestions { get; set; } = new();
        [BindProperty]
        public EventSuggestionViewModel Suggestion { get; set; } = new();

        public Guid SelectedSuggestion;
        public IActionResult OnGet()
        {
            if (Ctx.User.IsEventOrganizer)
            {
                var res = Manager.Event.GetSuggestions(Ctx.User.BranchId);

                if (res.IsUnSuccessful) return HandleError(res.Plain);

                Suggestions = res.Value;
            }

            return Page();
        }
        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid) return Page();

            var res = Manager.Event.Create(Event.New(Ctx.User.BranchId, Suggestion.Title, Suggestion.Description, Suggestion.Venue, true));
            if (res.IsUnSuccessful) return HandleError(res);

            return RedirectToPage("/Pages/Events/List");
        }
        public IActionResult OnPostDelete(Guid id)
        {
            var res = Manager.Event.Delete(id);
            if (res.IsUnSuccessful) return HandleError(res);

            return Page();
        }
        public IActionResult OnPostAccept(Guid id)
        {
            return RedirectToPage("/Pages/Events/Create", new { suggestion = id.ToString() });
        }
    }
}
