using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Components.CommentStates.Edit;

public partial class EditCommentComponent: ComponentBase
{
    [Inject]
    private ICommentService? CommentService { get; set; }

    [Parameter, EditorRequired]
    public Comment Comment { get; set; } = new();

    [Parameter]
    public bool ShowBackButton { get; set;}

    [Parameter, EditorRequired]
    public EventCallback<Comment> RaiseSave { get; set; }

    [Parameter, EditorRequired]
    public EventCallback<Comment> RaiseCancel { get; set; }

    private Comment EditComment { get; set; } = new();

    protected override void OnInitialized()
    {
        if (Comment != null)
        {
            EditComment = new()
            {
                Id = Comment.Id,
                Title = Comment.Title,
                Content = Comment.Content,
                SubmittedOn = Comment.SubmittedOn,
                PhotoId = Comment.PhotoId
            };
        }
    }

    private async Task HandleSaveClickAsync() => await RaiseSave.InvokeAsync(EditComment);
    private async Task HandleCancelClickAsync() => await RaiseCancel.InvokeAsync(Comment);
}