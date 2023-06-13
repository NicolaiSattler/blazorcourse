using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Shared.Interface;

public interface IPhotoService
{
    Task<Photo?> GetByIdAsync(int id);
    Task<ICollection<Photo>?> GetAllAsync();
    Task<Photo?> AddAsync(Photo newPhoto);
    Task<Photo?> UpdateAsync(Photo changedPhoto);
    Task<Photo?> DeleteAsync(int id);
}