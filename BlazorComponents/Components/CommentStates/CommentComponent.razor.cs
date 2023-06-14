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

    private async Task HandleConfirmDelete()
    {
        await OnDeleteComment.InvokeAsync();
        SetDefaultState();
    }

    private async Task HandleSave()
    {
        await OnEditComment.InvokeAsync();
        SetDefaultState();
    }

    private async Task HandleSaveNew()
    {
        await OnNewComment.InvokeAsync();
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