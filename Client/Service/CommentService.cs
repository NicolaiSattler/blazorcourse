using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Service;

public class CommentService : ICommentService
{
    private readonly ILogger<CommentService> _logger;

    private ICollection<Comment> Comments = new List<Comment>()
    {
        new() { Id = 1, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow },
        new() { Id = 2, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow },
        new() { Id = 3, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow },
        new() { Id = 5, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow }
    };

    public CommentService(ILogger<CommentService> logger)
    {
        _logger = logger;
    }

    public async Task<Comment?> AddAsync(Comment newComment)
    {
        Comments.Add(newComment);

        await Task.CompletedTask;

        return newComment;
    }

    public async Task<Comment?> DeleteAsync(int commentId)
    {
        var item = Comments.FirstOrDefault(c => c.Id == commentId);

        if (item != null)
            Comments.Remove(item);

        await Task.CompletedTask;

        return item;
    }

    public async Task<IList<Comment>?> GetByPhotoIdAsync(int photoId)
    {
        await Task.CompletedTask;

        return Comments.Where(c => c.PhotoId == photoId)
                       .ToList();
    }

    public async Task<Comment?> UpdateAsync(Comment changedComment)
    {
        var item = Comments.FirstOrDefault(c => c.Id == changedComment.Id);

        if (item != null)
        {
            Comments.Remove(item);
            Comments.Add(changedComment);
        }

        await Task.CompletedTask;

        return changedComment;
    }
}