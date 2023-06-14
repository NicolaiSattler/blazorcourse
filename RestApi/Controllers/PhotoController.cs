
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using MyBlazorCourse.Shared.Model;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhotoController : ControllerBase
{
    private readonly IPhotoRepository _repository;
    private readonly ILogger<PhotoController> _logger;

    public PhotoController(IPhotoRepository repository, ILogger<PhotoController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<Photo>> CreateAsync(Photo newPhoto)
    {
        if (newPhoto == null) return BadRequest();

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

    [HttpPut("{id}")]
    public async Task<ActionResult<Photo>> UpdateAsync(Photo changedPhoto)
    {
        var result = await _repository.UpdateAsync(changedPhoto);

        if (result == null) return NotFound(changedPhoto.Id);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Photo>> DeleteAsync(int id)
    {
        var result = await _repository.RemoveAsync(id);

        if (result == null) return NotFound(id);

        return result;
    }
}