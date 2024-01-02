using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Framework.src.Authorization
{
    public class AddressAdminOrOwnerHandler : AuthorizationHandler<AdminOrOwnerRequirement, AddressReadDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrOwnerRequirement requirement, AddressReadDTO resource)
        {
            ClaimsPrincipal claims = context.User;
            string userRole = claims.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;
            string userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

            if (userRole == UserRole.Admin.ToString()) context.Succeed(requirement);
            else if (userId == resource.User.ID.ToString()) context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }

    public class AddressAdminOrOwnerRequirement : IAuthorizationRequirement { }
}