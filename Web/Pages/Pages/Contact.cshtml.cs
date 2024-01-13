using Logic.Interfaces;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Middlewares.Authentication;

namespace Web.Pages.Pages
{
    public class ContactModel : PageModelWrapper
    {
        public ContactModel(IManager manager, IAuthenticationContext ctx) : base(manager, ctx) { }
        public bool HasError { get; private set; }
        public bool IsLoading { get; private set; }

        public List<User> Students = new();
        public User? EventOrganizer;
        public string Email = string.Empty;
        public IActionResult OnGet()
        {
            var res = Manager.User.GetBranchContacts(Ctx.User.BranchId);

            if (res.IsUnSuccessful) return HandleError(res.Plain);

            Students = res.Value.Where(u => u.IsStudentComitee).ToList();
            EventOrganizer = res.Value.Where(u => u.IsEventOrganizer).FirstOrDefault();

            var cRes = Manager.Credentials.GetById(Ctx.User.Id);

            if (cRes.IsSuccessful) Email = cRes.Value.Email;

            return Page();
        }
    }
}
