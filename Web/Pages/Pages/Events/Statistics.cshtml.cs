using Logic.Interfaces;
using Logic.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Web.Middlewares.Authentication;

namespace Web.Pages.Pages.Events
{
    public class StatisticsModel : PageModelWrapper
    {
        public StatisticsModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx, UserRole.EventOrganizer) { }
        public Dictionary<string, int> MonthlyStatistics { get; set; } = new();
        public List<Event> Events { get; private set; } = new();
        public Dictionary<string, Dictionary<EventParticipanceEnum, int>> UserStatistics { get; set; } = new();
        public IActionResult OnGet()
        {
            var resM = Manager.Event.GetCountEventsPerMonth();

            if (resM.IsUnSuccessful) return HandleError(resM.Plain);

            MonthlyStatistics = resM.Value;

            var resE = Manager.Event.GetAll();

            if (resE.IsUnSuccessful) return HandleError(resE.Plain);

            Events = resE.Value.Select(e => { e.SetSigned(Manager.Event.Participance.GetAllSignedForEvent(e.Id)); return e; }).ToList();

            var resU = Manager.Event.Participance.GetUserStatistics();

            if (resU.IsUnSuccessful) return HandleError(resU.Plain);

            foreach (var s in resU.Value)
            {
                var resUser = Manager.User.GetById(s.Key);
                if (resUser.IsSuccessful) UserStatistics.Add(resUser.Value.UserName, s.Value);
            }

            return Page();
        }
    }
}
