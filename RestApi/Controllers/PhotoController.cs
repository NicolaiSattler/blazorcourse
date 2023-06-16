
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace RestApi.Controllers;

//TODO: authorization uitwerken.

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IPhotoRepository _repository;
    private readonly Service.IPhotoService _photoService;
    private readonly ILogger<PhotoController> _logger;

    public PhotoController(IPhotoRepository repository,
                           Service.IPhotoService photoService,
                           ILogger<PhotoController> logger)
    {
        _repository = repository;
        _photoService = photoService;
        _logger = logger;
    }

    [HttpPost, Authorize]
    public async Task<ActionResult<Photo>> CreateAsync(Photo newPhoto)
    {
        return await _photoService.CreateAsync(newPhoto, User);
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