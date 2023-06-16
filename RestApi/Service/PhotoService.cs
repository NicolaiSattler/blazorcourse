using FluentValidation;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using MyBlazorCourse.Shared.Model;
using System.Security.Claims;

namespace RestApi.Service;

public class PhotoService : IPhotoService
{
    private readonly ILogger<PhotoService> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly IValidator<Photo> _validator;
    private readonly IPhotoRepository _repository;

    public PhotoService(ILogger<PhotoService> logger, 
                        IAuthorizationService authorizationService, 
                        IValidator<Photo> validator, 
                        IPhotoRepository repository)
    {
        _logger = logger;
        _authorizationService = authorizationService;
        _validator = validator;
        _repository = repository;
    }

    public async Task<ActionResult<Photo>> CreateAsync(Photo newPhoto, ClaimsPrincipal user)
    {
        if (newPhoto == null) return new BadRequestResult();

        var result = _validator.Validate(newPhoto);

        if (result!.IsValid)
        {
            return new BadRequestObjectResult(result.Errors);
        }

        newPhoto.UserName = user.Identity?.Name ?? "Unknown";

        return await _repository.AddAsync(newPhoto);
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