using GrpcService;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Service;

//TODO: resilienc
public class CommentService : ICommentService
{
    private readonly ILogger<CommentService> _logger;
    private readonly Commenter.CommenterClient _client;

    private ICollection<Comment> Comments = new List<Comment>()
    {
        new() { Id = 1, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow },
        new() { Id = 2, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow },
        new() { Id = 3, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow },
        new() { Id = 5, PhotoId = 1, Title = "Hello World", Content = "Foobar", SubmittedOn = DateTime.UtcNow }
    };

    public CommentService(ILogger<CommentService> logger, Commenter.CommenterClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<Comment?> AddAsync(Comment newComment)
    {
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

    public async Task<Comment?> DeleteAsync(int commentId)
    {
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