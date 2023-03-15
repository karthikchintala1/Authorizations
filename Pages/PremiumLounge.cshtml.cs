using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthorizationsTest.Pages
{
    [Authorize(Policy = "PremiumOnly")]
    public class PremiumLoungeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
