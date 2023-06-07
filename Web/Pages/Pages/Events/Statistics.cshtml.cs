using Logic;
using Logic.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Enums;

namespace Web.Pages.Pages.Events
{
    public class StatisticsModel : PageModelWrapper
    {
        public Dictionary<string, int> MonthlyStatistics { get; set; } = new();
        public List<Event> Events { get; private set; } = new();
        public Dictionary<string, Dictionary<EventParticipanceEnum, int>> UserStatistics { get; set; } = new();
        public IActionResult OnGet()
        {
            if (!Initiate()) return NotInitiated;

            var resM = Manager.Event.GetCountEventsPerMonth();

            if (!resM.IsSuccessful) return HandleError(resM.Plain);

            MonthlyStatistics = resM.Value;

            var resE = Manager.Event.GetAll();

            if (!resE.IsSuccessful) return HandleError(resE.Plain);

            Events = resE.Value.Select(e => { e.SetSigned(Manager.Event.Participance.GetAllSignedForEvent(e.Id)); return e; }).ToList();

            var resU = Manager.Event.Participance.GetUserStatistics();

            if (!resU.IsSuccessful) return HandleError(resU.Plain);

            foreach (var s in resU.Value)
            {
                var resUser = Manager.User.GetById(s.Key);
                if (resUser.IsSuccessful && resUser.Value != null) UserStatistics.Add(resUser.Value.UserName, s.Value);
            }

            return Page();
        }
    }
}
