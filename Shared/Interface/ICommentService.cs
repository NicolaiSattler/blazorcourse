using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Shared.Interface;

public interface ICommentService
{
    Task<IList<Comment>?> GetByPhotoIdAsync(int photoId);
    Task<Comment?> AddAsync(Comment newComment);
    Task<Comment?> UpdateAsync(Comment changedComment);
    Task<Comment?> DeleteAsync(int commnetId);
}