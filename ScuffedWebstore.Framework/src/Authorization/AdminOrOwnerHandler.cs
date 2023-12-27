using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Framework.src.Authorization
{
    public class AdminOrOwnerHandler : AuthorizationHandler<AdminOrOwnerRequirement, OwnedEntity>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrOwnerRequirement requirement, OwnedEntity resource)
        {
            try
            {
                Console.WriteLine("something");
                ClaimsPrincipal claims = context.User;
                string userRole = claims.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;
                string userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;

                Console.WriteLine(userId);
                Console.WriteLine(UserRole.Admin.ToString());
                Console.WriteLine(userRole == UserRole.Admin.ToString());

                if (userRole == UserRole.Admin.ToString()) context.Succeed(requirement);
                else if (userId == resource.UserID.ToString()) context.Succeed(requirement);

                return Task.CompletedTask;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return Task.CompletedTask;
        }
    }

    public class AdminOrOwnerRequirement : IAuthorizationRequirement { }
}