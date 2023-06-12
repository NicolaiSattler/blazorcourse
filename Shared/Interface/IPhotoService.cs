using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Shared.Interface;

public interface IPhotoService
{
    Task<Photo> GetByIdAsync(int id);
    Task<ICollection<Photo>> GetAllAsync();
    Task AddAsync(Photo newPhoto);
}