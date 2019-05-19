using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using planty_compare_portal.Data;

namespace planty_compare_portal.Authorization
{
    public class PurchasingPowerManagerAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, PurchasingPower>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            PurchasingPower resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            if (requirement.Name != Constants.ApproveOperationName &&
                requirement.Name != Constants.RejectOperationName)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or reject.
            if (context.User.IsInRole(Constants.PurchasingPowerManagerRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
