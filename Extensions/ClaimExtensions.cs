using System.Security.Claims;

namespace AuthorizationsTest.Extensions
{
    public class ClaimExtensions
    {
        public static Claim GetClaim(ClaimsPrincipal principal, params string[] types)
        {
            foreach (string type in types)
            {
                var claim = principal.Claims.FirstOrDefault(c => string.Equals(c.Type, type, StringComparison.OrdinalIgnoreCase));
                if (claim != null)
                    return claim;
            }
            return null;
        }

    }
}
