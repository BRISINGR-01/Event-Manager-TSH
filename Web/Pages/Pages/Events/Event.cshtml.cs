using Logic;
using Logic.Models;
using Logic.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace Web.Pages.Events
{
    [AllowAnonymous]
    public class EventModel : PageModelWrapper
    {
        [BindProperty(SupportsGet = true)]
        public string? Id { get; set; }
        public Event? Event { get; private set; }
        public List<User> SignedStudents { get; set; } = new();
        public bool IsSigned = false;
        public IActionResult OnGet()
        {
            if (!Initiate()) return NotInitiated;

            if (Guid.TryParse(Id, out var eventGuid))
            {           
                var res = Manager.Event.GetBy(eventGuid);
                if (res.IsSuccessful && res.Value != null)
                {
                    Event = res.Value;
                    var presenceRes = Manager.Event.Participance.GetEventUserState(eventGuid, User.Id);
                    if (presenceRes.IsSuccessful && presenceRes.Value != null)
                    {
                        IsSigned = presenceRes.Value.State == EventParticipanceEnum.Signed;
                    }
                } else if (res.Value == null)
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
            if (!Initiate()) return NotInitiated;

            if (User.IsEventOrganizer) return NotInitiated;

            if (Guid.TryParse(Id, out var eventGuid))
            {
                var res = Manager.Event.AlterParticipance(User.Id, eventGuid, EventParticipanceEnum.Signed);
               
                if (!res.IsSuccessful)
                {
                    return res.ErrorIsDefault ? 
                        HandleError("There was a problem with signing you up for the event") : 
                        HandleError(res.Plain);
                }
            }

            return OnGet();
        }
        public IActionResult OnPostUnSign()
        {
            if (!Initiate()) return NotInitiated;

            if (User.IsEventOrganizer) return NotInitiated;

            if (Guid.TryParse(Id, out var eventGuid))
            {
                var res = Manager.Event.AlterParticipance(User.Id, eventGuid, EventParticipanceEnum.None);
                
                if (!res.IsSuccessful)
                {
                    return res.ErrorIsDefault ? 
                        HandleError("There was a problem while canceling you for the event") :
                        HandleError(res.Plain);
                }
            }

            return OnGet();
        }
    }
}
