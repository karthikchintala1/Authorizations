using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthorizationsTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IAuthorizationService _authorizationService;

        public IndexModel(ILogger<IndexModel> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public async Task OnGet()
        {
            var isPremium = await _authorizationService.AuthorizeAsync(User, "PremiumOnly");
            var isStandard = await _authorizationService.AuthorizeAsync(User, "StandardOnly");

            ViewData["hasPremiumAccess"] = isPremium.Succeeded;
            ViewData["hasStandardAccess"] = isStandard.Succeeded;
        }
    }
}