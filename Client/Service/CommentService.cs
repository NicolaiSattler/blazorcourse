using GrpcService;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Service;

//TODO: resilienc
public class CommentService : ICommentService
{
    private readonly ILogger<CommentService> _logger;
    private readonly Commenter.CommenterClient _client;

    public List<Comment> StubComments = new List<Comment>
    {
        new() { Id = 1, PhotoId = 1, Title = "Test 1", Content = "Content goes here.." },
        new() { Id = 2, PhotoId = 1, Title = "Test 2", Content = "Content goes here.." },
        new() { Id = 3, PhotoId = 1, Title = "Test 3", Content = "Content goes here.." },
        new() { Id = 4, PhotoId = 1, Title = "Test 4", Content = "Content goes here.." },
        new() { Id = 5, PhotoId = 1, Title = "Test 5", Content = "Content goes here.." },
        new() { Id = 6, PhotoId = 1, Title = "Test 6", Content = "Content goes here.." },
    };

    public CommentService(ILogger<CommentService> logger, Commenter.CommenterClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<Comment?> AddAsync(Comment newComment)
    {
        try
        {
            StubComments.Add(newComment);

            await Task.CompletedTask;

            return newComment;

            var request = new CreateRequest()
            {
                Photoid = newComment.PhotoId,
                Title = newComment.Title,
                Content = newComment.Content,
            };

            var response = await _client.CreateAsync(request);

            return new()
            {
                Id = response.Id,
                PhotoId = response.Photoid,
                Title = response.Title,
                Content = response.Content,
                SubmittedOn = response.Submittedon.ToDateTime()
            };
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        return null;
    }

    public async Task<Comment?> DeleteAsync(int commentId)
    {
        if (StubComments != null)
            Console.WriteLine("stub is not null");

        var item = StubComments?.FirstOrDefault(c => c.Id == commentId);
        if (item != null)
        {
            StubComments.Remove(item);
            return item;
        }
        else
        {
            return null;
        }

        var request = new DeleteRequest() { Commentid = commentId };
        var response = await _client.DeleteAsync(request);
        return new()
        {
            Id = response.Id,
            PhotoId = response.Photoid,
            Title = response.Title,
            Content = response.Content,
            SubmittedOn = response.Submittedon.ToDateTime()
        };
    }

    public async Task<IList<Comment>?> GetByPhotoIdAsync(int photoId)
    {
        try
        {
            return StubComments.Where(c => c.PhotoId == photoId).ToList();

            var request = new GetByPhotoIdRequest() { Photoid = photoId };
            var response = await _client.GetByPhotoIdAsync(request);

            return response.Comments.Select(c => new Comment()
            {
                Id = c.Id,
                PhotoId = c.Photoid,
                Title = c.Title,
                Content = c.Content,
                SubmittedOn = c.Submittedon.ToDateTime()
            }).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
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

        var request = new UpdateRequest()
        {
            Id = changedComment.Id,
            Title = changedComment.Title,
            Content = changedComment.Content,
            Photoid = changedComment.PhotoId
        };
        var response = await _client.UpdateAsync(request);


        return new()
        {
            Id = response.Id,
            PhotoId = response.Photoid,
            Title = response.Title,
            Content = response.Content,
            SubmittedOn = response.Submittedon.ToDateTime()
        };
    }
}