using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Components.Comments;

public partial class CommentsComponent: ComponentBase
{
    [Inject]
    private ICommentService? CommentService { get; set; }

    [Parameter, EditorRequired]
    public int PhotoId { get; set; }

    public IList<Comment> CommentCollection { get; set;} = new List<Comment>();

    protected async override Task OnInitializedAsync()
    {
        if (CommentService != null)
            CommentCollection = await CommentService.GetByPhotoIdAsync(PhotoId)
                              ?? new List<Comment>();
    }

    private async Task HandleOnDeleteAsync(Comment deletedComment)
    {
        if (deletedComment == null) return;

        if (CommentService != null)
            await CommentService.DeleteAsync(deletedComment.Id);

        CommentCollection.Remove(deletedComment);
    }

    private async Task HandleOnNewAsync(Comment newComment)
    {
        if (newComment == null) return;

        newComment.PhotoId = PhotoId;

        if (CommentService != null)
            await CommentService.AddAsync(newComment);

        CommentCollection.Add(newComment);
    }

    private async Task HandleOnEditAsync(Comment changedComment)
    {
        if (changedComment == null) return;

        if (CommentService != null)
            await CommentService.UpdateAsync(changedComment);

        var oldComment = CommentCollection.First(c => c.Id == changedComment.Id);
        var index = CommentCollection.IndexOf(oldComment);

        CommentCollection[index] = changedComment;
    }
}