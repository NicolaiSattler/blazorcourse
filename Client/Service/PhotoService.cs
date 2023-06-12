using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Service;

public class PhotoService : IPhotoService
{
    private ICollection<Photo> _collection = new List<Photo>()
    {
        new() { Id = 1, Title = "First", Description = "My First Photo"},
        new() { Id = 2, Title = "Second", Description = "My Second Photo"},
        new() { Id = 3, Title = "Third", Description = "My Third Photo"}
    };

    public async Task<Photo> GetByIdAsync(int id)
    {
        await Task.CompletedTask;

        return _collection.First(p => p.Id == id);
    }

    public async Task<ICollection<Photo>> GetAllAsync()
    {
        await Task.CompletedTask;

        return _collection;
    }

    public async Task AddAsync(Photo newPhoto)
    {
        _collection.Add(newPhoto);

        await Task.CompletedTask;
    }
}