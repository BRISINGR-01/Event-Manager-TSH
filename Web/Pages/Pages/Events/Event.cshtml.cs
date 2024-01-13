using Logic.Interfaces;
using Logic.Models;
using Logic.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Web.Middlewares.Authentication;

namespace Web.Pages.Events
{
    public class EventModel : PageModelWrapper
    {
        public EventModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }
        public Event? Event { get; private set; }
        public List<User> SignedStudents { get; set; } = new();
        public bool IsSigned = false;
        public IActionResult OnGet()
        {
            if (Guid.TryParse(Id, out var eventGuid))
            {
                var res = Manager.Event.GetBy(eventGuid);
                if (res.IsSuccessful)
                {
                    Event = res.Value;
                    var presenceRes = Manager.Event.Participance.GetEventUserParticipance(eventGuid, Ctx.User.Id);
                    if (presenceRes.IsSuccessful)
                    {
                        IsSigned = presenceRes.Value.State == EventParticipanceEnum.Signed;
                    }
                }
                else if (res.Value == null)
                {
                    Error = "Cannot find event!";
                    return Page();
                }
                else
                {
                    return HandleError(res.Plain);
                }

                return Page();
            }

            return RedirectToPage("/Pages/Events/List");
        }

        public IActionResult OnPostSignUp()
        {
            if (Ctx.User.IsEventOrganizer) return RedirectToLogIn;

            if (Guid.TryParse(Id, out var eventGuid))
            {
                var res = Manager.Event.AlterParticipance(Ctx.User.Id, eventGuid, EventParticipanceEnum.Signed);

                if (res.IsUnSuccessful)
                {
                    return HandleError(res);
                }
            }

            return OnGet();
        }
        public IActionResult OnPostUnSign()
        {
            if (Ctx.User.IsEventOrganizer) return RedirectToLogIn;

            if (Guid.TryParse(Id, out var eventGuid))
            {
                var res = Manager.Event.AlterParticipance(Ctx.User.Id, eventGuid, EventParticipanceEnum.None);

                if (res.IsUnSuccessful)
                {
                    return HandleError(res);
                }
            }

            return OnGet();
        }
    }
}
