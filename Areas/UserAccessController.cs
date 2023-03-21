using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationsTest.Areas
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccessController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public UserAccessController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<(bool isPremium, bool isStandard)>> GetUserPermissionsAsync()
        {
            var isPremium = await _authorizationService.AuthorizeAsync(User, "PremiumOnly");
            var isStandard = await _authorizationService.AuthorizeAsync(User, "StandardOnly");

            return Ok(new Tuple<bool, bool>(isPremium.Succeeded, isStandard.Succeeded));
        }
    }
}
