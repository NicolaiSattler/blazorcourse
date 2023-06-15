using Microsoft.AspNetCore.Authorization;
using MyBlazorCourse.Shared.Model;

namespace RestApi.Authorization
{
    public class UpdatePhotoAuthorizationHandler : AuthorizationHandler<UpdatePhotoAuthorizationRequirement, Photo>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UpdatePhotoAuthorizationRequirement requirement, Photo resource)
        {
            if (context.User?.Identity?.Name == resource.UserName)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
