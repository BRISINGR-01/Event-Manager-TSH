using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Diagnostics;

namespace Web.Pages.Errors
{
    public class _404Model : PageModelWrapper
    {
        public void OnGet()
        {
            Initiate();
        }
    }
}
