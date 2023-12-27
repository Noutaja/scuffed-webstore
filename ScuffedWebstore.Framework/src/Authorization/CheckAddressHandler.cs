using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Framework.src.Authorization
{
    public class CheckAddressHandler : AuthorizationHandler<AdminOrOwnerRequirement, AddressReadDTO>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrOwnerRequirement requirement, AddressReadDTO resource)
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
}