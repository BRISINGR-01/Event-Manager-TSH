using Logic;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shared.Enums;
using Web.ViewModels;

namespace Web.Pages.Pages
{
    public class ContactModel : PageModelWrapper
    {
        public bool HasError { get; private set; }
        public bool IsLoading { get; private set; }

        public List<User> Students = new();
        public User? EventOrganizer;
        public IActionResult OnGet()
        {
            if (!Initiate()) return NotInitiated;

            var res = Manager.User.GetBranchContacts();

            if (!res.IsSuccessful) return HandleError(res.Plain);

            Students = res.Value.Where(u => u.Role == UserRole.StudentComitee).ToList();
            EventOrganizer = res.Value.Where(u => u.IsEventOrganizer).FirstOrDefault();

            return Page();
        }
    }
}
