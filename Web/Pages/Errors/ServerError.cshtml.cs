using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Errors
{
    public class ServerErrorModel : PageModelWrapper
    {
        public void OnGet()
        {
            Initiate();
        }
    }
}
