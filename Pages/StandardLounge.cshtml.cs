using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthorizationsTest.Pages
{
    [Authorize(Policy = "StandardOnly")]
    public class StandardLoungeModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
