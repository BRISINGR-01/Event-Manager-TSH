using Domain.Managers;
using Infrastructure.DatabaseManagers;
using Logic.Models;
using Logic.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Globalization;
using System;

namespace Web.Pages.Events
{
    [Authorize]
    public class ListModel : PageModelWrapper
    {
        [BindProperty(SupportsGet = true)]
        public int Month { get; set; }
        
        public List<Event> Events = new();
        public List<string> MonthNames = DateTimeFormatInfo.CurrentInfo.MonthNames.ToList();
        public IActionResult OnGet()
        {
            if (!Initiate()) return NotInitiated;

            try
            {
                var res = Manager.Event.GetAllOfMonth(Month);
                if (res.IsSuccessful)
                {
                    Events = res.Value;

                    if (User.IsEventOrganizer)
                    {
                        foreach (var e in Events)
                        {
                            e.SetSigned(Manager.Event.Participance.GetAllSignedForEvent(e.Id));
                        }
                    }
                } else
                {
                    return HandleError(res.Plain);
                }
            } catch (Exception ex)
            {
                Error = ex.Message;
            }

            return Page();
        }
    }
}
