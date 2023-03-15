using Microsoft.AspNetCore.Authorization;

namespace AuthorizationsTest.Authorization.ProductAccess
{
    public class ProductAccessRequirement : IAuthorizationRequirement
    {
        public string[] ProductIds { get; set; }

        public ProductAccessRequirement(string[] productIds)
        {
            ProductIds = productIds;
        }
    }
}
