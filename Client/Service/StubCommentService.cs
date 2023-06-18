using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Service;

public class StubCommentService : ICommentService
{
    public List<Comment> StubComments = new List<Comment>
    {
        new() { Id = 1, PhotoId = 1, Title = "Test 1", Content = "Content goes here..", UserName = "Bob"},
        new() { Id = 2, PhotoId = 1, Title = "Test 2", Content = "Content goes here..", UserName = "Bob"},
        new() { Id = 3, PhotoId = 1, Title = "Test 3", Content = "Content goes here..", UserName = "Bob"},
        new() { Id = 4, PhotoId = 1, Title = "Test 4", Content = "Content goes here..", UserName = "Bob"},
        new() { Id = 5, PhotoId = 1, Title = "Test 5", Content = "Content goes here..", UserName = "Bob"},
        new() { Id = 6, PhotoId = 1, Title = "Test 6", Content = "Content goes here..", UserName = "Bob"},
    };

    public async Task<Comment?> AddAsync(Comment newComment)
    {
        StubComments.Add(newComment);

        await Task.CompletedTask;

        return newComment;
    }

    public async Task<Comment?> DeleteAsync(int commentId)
    {
        var item = StubComments?.FirstOrDefault(c => c.Id == commentId);
        if (item != null)
        {
            StubComments?.Remove(item);
            return item;
        }
        else
        {
            return null;
        }
    }

    public async Task<IList<Comment>?> GetByPhotoIdAsync(int photoId)
    {
        var result = StubComments.Where(c => c.PhotoId == photoId).ToList();

        await Task.CompletedTask;

        return result;
    }

    public async Task<Comment?> UpdateAsync(Comment changedComment)
    {
        if (changedComment == null) return null;

        var item = StubComments.FirstOrDefault(c => c.Id == changedComment.Id);

        if (item != null)
        {
            StubComments.Remove(item);
            StubComments.Add(item);

            return changedComment;
        }
        else
        {
            return changedComment;
        }
    }
}
