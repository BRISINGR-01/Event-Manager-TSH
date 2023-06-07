using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Pages
{
    public class CodeModel : PageModelWrapper
    {
        public string Id { get; private set; } = string.Empty;
        public IActionResult OnGetAsync()
        {
            if (!Initiate()) return NotInitiated;

            if (User.IsEventOrganizer) return RedirectToPage("/Pages/CheckCode");

            Id = User.Id.ToString();

            return Page();
        }
    }
}
