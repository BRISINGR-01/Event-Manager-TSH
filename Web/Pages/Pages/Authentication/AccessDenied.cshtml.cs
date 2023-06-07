using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Pages.Authentication
{
    [AllowAnonymous]
    public class AccessDeniedModel : PageModelWrapper
    {
        
    }
}
