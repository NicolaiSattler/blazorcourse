using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Components.CommentStates;

public partial class CommentComponent: ComponentBase
{
    [Parameter]
    public Comment? Comment { get; set; }

    [Parameter]
    public EventCallback<Comment> OnDeleteComment { get; set; }

    [Parameter]
    public EventCallback<Comment> OnNewComment { get; set; }

    [Parameter]
    public EventCallback<Comment> OnEditComment { get; set; }

    [Parameter]
    public CommentState State { get; set; } = CommentState.Read;

    private void HandleEdit() => State = CommentState.Edit;

    private void HandleDelete() => State = CommentState.Delete;

    private void HandleCancel() => SetDefaultState();

    private async Task HandleConfirmDelete(Comment? deletedComment)
    {
        await OnDeleteComment.InvokeAsync(deletedComment);
        SetDefaultState();
    }

    private async Task HandleSave(Comment? changedComment)
    {
        await OnEditComment.InvokeAsync(changedComment);
        SetDefaultState();
    }

    private async Task HandleSaveNew(Comment? newComment)
    {
        await OnNewComment.InvokeAsync(newComment);
        SetDefaultState();
    }

    private void SetDefaultState()  => State = CommentState.Read;
}

public enum CommentState
{
    Undefined = 0,
    New = 1,
    Read = 2,
    Edit = 3,
    Delete = 4,
}