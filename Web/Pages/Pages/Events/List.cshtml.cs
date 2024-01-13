using Logic.Interfaces;
using Logic.Models.Events;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using System.Globalization;
using Web.Middlewares.Authentication;

namespace Web.Pages.Events
{
    public class ListModel : PageModelWrapper
    {
        public ListModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
        [BindProperty(SupportsGet = true)]
        public EventsFilterEnum? Filter { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Month { get; set; }

        public List<Event> Events = new();
        public List<string> MonthNames = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();
        public IActionResult OnGet()
        {
            try
            {
                var res2 = Manager.Event.GetAll();
                var res = Manager.Event.GetAllOfMonth(Month, Ctx.User.BranchId, Filter);
                if (res.IsSuccessful)
                {
                    Events = res.Value;

                    if (Ctx.User.IsEventOrganizer)
                    {
                        foreach (var e in Events)
                        {
                            e.SetSigned(Manager.Event.Participance.GetAllSignedForEvent(e.Id));
                        }
                    }
                }
                else
                {
                    return HandleError(res.Plain);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return Page();
        }
        public string MonthName => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(new DateTime(DateTime.Now.Year, (DateTime.Now.Month + Month == 0 ? 12 : DateTime.Now.Month + Month < 0 ? 12 : 0) + DateTime.Now.Month + Month, 1).Month);
    }
}
