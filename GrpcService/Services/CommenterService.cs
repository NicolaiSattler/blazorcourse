using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using MyBlazorCourse.Shared.Model;

namespace GrpcService.Services;

public class CommenterService: Commenter.CommenterBase
{
    private readonly ICommentRepository _commentRepository;

    public CommenterService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [Authorize]
    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        try
        {
            var newComment = new Comment
            {
                PhotoId = request.Photoid,
                Title = request.Title,
                Content = request.Content,
                SubmittedOn = DateTime.UtcNow
            };

            newComment = await _commentRepository.AddAsync(newComment);

            var response = newComment is not null ? new CreateResponse()
            {
                Id = newComment.Id,
                Photoid = newComment.PhotoId,
                Title = newComment.Title,
                Content = newComment.Content,
                Submittedon = Timestamp.FromDateTime(newComment.SubmittedOn.ToUniversalTime())
            }
            : new CreateResponse();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return new CreateResponse();
    }
    
    [Authorize]
    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        var comment = new Comment
        {
            Id = request.Id,
            PhotoId = request.Photoid,
            Title = request.Title,
            Content = request.Content,
            SubmittedOn = DateTime.UtcNow
        };

        var changedComment = await _commentRepository.UpdateAsync(comment);

        return changedComment is not null ? new UpdateResponse
        {
            Id = changedComment.Id,
            Title = changedComment.Title,
            Content = changedComment.Content,
            Photoid = changedComment.PhotoId,
            Submittedon = Timestamp.FromDateTime(changedComment.SubmittedOn.ToUniversalTime())
        }
        : new UpdateResponse();
    }

    [Authorize]
    public async override Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var deletedComment = await _commentRepository.RemoveAsync(request.Commentid);

        return deletedComment is not null ? new DeleteResponse
        {
            Id = deletedComment.Id,
            Title = deletedComment.Title,
            Content = deletedComment.Content,
            Photoid = deletedComment.PhotoId,
            Submittedon = Timestamp.FromDateTime(deletedComment.SubmittedOn.ToUniversalTime())
        }
        : new DeleteResponse();
    }

    public async override Task<GetByPhotoIdResponse> GetByPhotoId(GetByPhotoIdRequest request, ServerCallContext context)
    {
        var result = await _commentRepository.GetByPhotoIdAsync(request.Photoid);

        var items = result?.Select(r => new GetByPhotoIdItem()
        {
            Id = r.Id,
            Photoid = r.PhotoId,
            Title = r.Title,
            Content = r.Content,
            Submittedon = Timestamp.FromDateTime(r.SubmittedOn.ToUniversalTime())
        });

        var response = new GetByPhotoIdResponse();
        response.Comments.AddRange(items);

        return response;
    }
}