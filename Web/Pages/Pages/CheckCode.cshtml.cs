using Logic;
using Logic.Models;
using Logic.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Enums;

namespace Web.Pages.Pages
{
    public class CheckCodeModel : PageModelWrapper
    {
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }
        public List<Event> Events { get; private set; } = new();
        public User? CheckedUser { get; private set; }
        public bool IsSigned { get; private set; }
        public IActionResult OnGet()
        {
            if (!Initiate()) return NotInitiated;

            var res = Manager.Event.GetOnGoing();

            if (!res.IsSuccessful) return HandleError(res.Plain);

            Events = res.Value;

            if (Guid.TryParse(Id, out var userId) && Events.Count != 0)
            {
                var userRes = Infrastructure.Manager.UserGet(userId);

                if (!userRes.IsSuccessful) return HandleError(res.Plain);

                CheckedUser = userRes.Value;

                if (CheckedUser == null)
                {
                    Error = "Couldn't find user";
                    return Page();
                }

                foreach (var Event in Events)
                {
                    var participanceRes = Manager.Event.Participance.GetEventUserState(Event.Id, userId);

                    if (!participanceRes.IsSuccessful)
                    {
                        if (participanceRes.ErrorIsDefault)
                        {
                            Error = "Sorry, but an error occured while checking the code";
                            return Page();
                        }
                        else
                        {
                            return HandleError(participanceRes.Plain);
                        }
                    }

                    if (participanceRes.Value?.State == EventParticipanceEnum.Signed)
                    {
                        IsSigned = true;
                        Manager.Event.AlterParticipance(userId, Event.Id, EventParticipanceEnum.Present);
                    }
                    else if (participanceRes.Value?.State == EventParticipanceEnum.Present)
                    {
                        IsSigned = true;
                        Manager.Event.AlterParticipance(userId, Event.Id, EventParticipanceEnum.Present);
                    }
                    return Page();
                }
            }
            else if (Id != null)
            {
                return RedirectToPage("/Pages/CheckCode", new { id = (string?)null });
            }

            return Page();
        }
    }
}
