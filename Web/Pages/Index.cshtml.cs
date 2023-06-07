using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
	public class IndexModel : PageModelWrapper
	{
		public IActionResult OnGet()
		{
			
			if (!Initiate()) return NotInitiated;
			return RedirectToPage("/Pages/Events/List");
		}
	}
}