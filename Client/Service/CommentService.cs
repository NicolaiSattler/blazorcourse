using GrpcService;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Service;

//TODO: Add Ciruit Breaker + retry.
public class CommentService : ICommentService
{
    private readonly ILogger<CommentService> _logger;
    private readonly Commenter.CommenterClient _client;

    public CommentService(ILogger<CommentService> logger, Commenter.CommenterClient client)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<Comment?> AddAsync(Comment newComment)
    {
        try
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
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        return null;
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
        if (changedComment == null) return null;

        var response = await _client.UpdateAsync(new()
        {
            Id = changedComment.Id,
            Title = changedComment.Title,
            Content = changedComment.Content,
            Photoid = changedComment.PhotoId
        });

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