﻿using AuthorizationsTest.Core.UserAccess;
using AuthorizationsTest.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace AuthorizationsTest.Authorization.ProductAccess
{
    public class ProductAccessHandler : AuthorizationHandler<ProductAccessRequirement>
    {
        private readonly IUserAccessRepository _userAccessRepository;
        public ProductAccessHandler(IUserAccessRepository userAccessRepository)
        {
            _userAccessRepository = userAccessRepository;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProductAccessRequirement requirement)
        {
            var user = ClaimExtensions.GetClaim(context.User, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "nameidentifier");
            if (user == null) context.Fail();

            var hasAccess = _userAccessRepository.HasAccess(user.Value, requirement.ProductIds, CancellationToken.None);
            if (hasAccess) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
