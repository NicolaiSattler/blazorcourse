using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlazorCourse.Shared.Model;
using System.Security.Claims;

namespace RestApi.Service;

public class PhotoService : IPhotoService
{
    private readonly ILogger<PhotoService> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly IPhotoRepository _repository;

    public PhotoService(ILogger<PhotoService> logger, IAuthorizationService authorizationService, IPhotoRepository repository)
    {
        _logger = logger;
        _authorizationService = authorizationService;
        _repository = repository;
    }

    public async Task<ActionResult<Photo>> UpdateAsync(Photo photo, ClaimsPrincipal user)
    {
        if (photo == null)
        {
            return new BadRequestResult();
        }

        var authorizationResult = await _authorizationService.AuthorizeAsync(user, photo, "UpdatePhoto");

        if (authorizationResult?.Succeeded ?? false)
        {
            return await _repository.AddAsync(photo);
        }
        else
        {
            return new ForbidResult();
        }
    }
}