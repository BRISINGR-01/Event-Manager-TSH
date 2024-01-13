using Logic.Interfaces;
using Logic.Models;
using Logic.Models.Events;
using Logic.Utilities;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Web.Middlewares.Authentication;

namespace Web.Pages.Pages
{
    public class CheckCodeModel : PageModelWrapper
    {
        public CheckCodeModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx, UserRole.Student) { }
        [BindProperty(SupportsGet = true)]
        public string? Code { get; set; }
        public List<Event> Events { get; private set; } = new();
        public User? CheckedUser { get; private set; }
        public bool HasUserSigned { get; private set; } = false;
        public IActionResult OnGet()
        {
            var eventRes = Manager.Event.GetOnGoing(Ctx.User.BranchId);

            if (!eventRes.IsSuccessful) return HandleError(eventRes.Plain);

            Events = eventRes.Value;

            if (Events.Count == 0 || Code == null) return Page();

            var result = Encryption.Decrypt(Code);

            if (result == null) return Page();

            var userRes = Manager.User.GetById(Guid.Parse(result));

            if (userRes.IsUnSuccessful) return Page();

            CheckedUser = userRes.Value;

            foreach (var Event in Events)
            {
                var participanceRes = Manager.Event.Participance.GetEventUserParticipance(Event.Id, userRes.Value.Id);

                if (participanceRes.IsUnSuccessful) return HandleError(participanceRes.Plain);

                if (participanceRes.Value.State == EventParticipanceEnum.Signed)
                {
                    HasUserSigned = true;
                    Manager.Event.AlterParticipance(userRes.Value.Id, Event.Id, EventParticipanceEnum.Present);
                }
                else if (participanceRes.Value.State == EventParticipanceEnum.Present)
                {
                    HasUserSigned = true;
                    Manager.Event.AlterParticipance(userRes.Value.Id, Event.Id, EventParticipanceEnum.Present);
                }
            }

            return Page();
        }
    }
}
