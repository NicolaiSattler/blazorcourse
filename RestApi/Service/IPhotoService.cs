using Microsoft.AspNetCore.Mvc;
using MyBlazorCourse.Shared.Model;
using System.Security.Claims;

namespace RestApi.Service
{
    public interface IPhotoService
    {
        Task<ActionResult<Photo>> UpdateAsync(Photo photo, ClaimsPrincipal user);
    }
}