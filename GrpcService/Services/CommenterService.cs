using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MyBlazorCourse.Shared.Model;

namespace GrpcService.Services;

public class CommenterService: Commenter.CommenterBase
{
    public override Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var newComment = new Comment
        {
            PhotoId = request.Photoid,
            Title = request.Title,
            Content = request.Content,
        };

        //TOOD: EF Core save...

        var response = new CreateResponse()
        {
            Id = newComment.Id,
            Photoid = newComment.PhotoId,
            Title = newComment.Title,
            Content = newComment.Content,
            Submittedon = Timestamp.FromDateTime(newComment.SubmittedOn.ToUniversalTime())
        };

        return Task.FromResult(response);
    }

    public override Task<GetByPhotoIdResponse> GetByPhotoId(GetByPhotoIdRequest request, ServerCallContext context)
    {
        var result = new List<Comment>();
        //TODO: EF Core get by id;
        var items = result.Select(r => new GetByPhotoIdItem()
        {
            Id = r.Id,
            Photoid = r.PhotoId,
            Title = r.Title,
            Content = r.Content,
            Submittedon = Timestamp.FromDateTime(r.SubmittedOn.ToUniversalTime())
        });

        var response = new GetByPhotoIdResponse();
        response.Comments.AddRange(items);

        return Task.FromResult(response);
    }
}