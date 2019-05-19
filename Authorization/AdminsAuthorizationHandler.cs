using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using planty_compare_portal.Data;

namespace planty_compare_portal.Authorization
{
    public class AdminsAuthorizationHandler
                    : AuthorizationHandler<OperationAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can do anything.
            if (context.User.IsInRole(Constants.AdminRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
