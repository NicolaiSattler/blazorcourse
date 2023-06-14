using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Components.CommentStates.Delete;

public partial class DeleteCommentComponent: ComponentBase
{
    [Inject]
    private ICommentService? CommentService { get; set; }

    [Parameter, EditorRequired]
    public  Comment? Comment { get; set;}

    [Parameter, EditorRequired]
    public EventCallback<Comment> RaiseConfirmDelete { get; set; }

    [Parameter, EditorRequired]
    public EventCallback<Comment> RaiseCancel { get; set; }

    private async Task HandleConfirmDeleteClickAsync() => await RaiseConfirmDelete.InvokeAsync(Comment);
    private async Task HandleCancelClickAsync() => await RaiseCancel.InvokeAsync(Comment);
}