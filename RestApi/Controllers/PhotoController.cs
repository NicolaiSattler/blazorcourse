
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlazorCourse.Shared.Model;

namespace RestApi.Controllers;

//TODO: authorization uitwerken.

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IPhotoRepository _repository;
    private readonly IAuthorizationService _authService;
    private readonly ILogger<PhotoController> _logger;

    public PhotoController(IPhotoRepository repository, IAuthorizationService authService, ILogger<PhotoController> logger)
    {
        _repository = repository;
        _authService = authService;
        _logger = logger;
    }

    [HttpPost, Authorize]
    public async Task<ActionResult<Photo>> CreateAsync(Photo newPhoto)
    {
        if (newPhoto == null) return BadRequest();

        newPhoto.UserName = User.Identity?.Name ?? "Unknown";

        return await _repository.AddAsync(newPhoto);
    }

    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Photo>>> GetAsync()
    {
        var result = await _repository.GetAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Photo?>> GetByIdAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result == null) return NotFound(id);

        return Ok(result);
    }

    [HttpPut("{id}"), Authorize]
    public async Task<ActionResult<Photo>> UpdateAsync(Photo changedPhoto)
    {
        var result = await _repository.UpdateAsync(changedPhoto);

        return Ok(result);
    }

    [HttpDelete("{id}"), Authorize]
    public async Task<ActionResult<int>> DeleteAsync(int id)
    {
        var result = await _repository.RemoveAsync(id);

        return Ok(result);
    }
}