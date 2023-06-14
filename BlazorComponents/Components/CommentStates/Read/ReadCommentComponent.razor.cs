using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Components.CommentStates.Read;

public partial class ReadCommentComponent: ComponentBase
{
    [Inject]
    private ICommentService? CommentService { get; set; }

    [Parameter, EditorRequired]
    public Comment? Comment { get; set;}

    [Parameter, EditorRequired]
    public EventCallback<Comment> RaiseEdit { get; set; }

    [Parameter, EditorRequired]
    public EventCallback<Comment> RaiseDelete { get; set; }

    private async Task HandleDeleteClickAsync() => await RaiseDelete.InvokeAsync(Comment);
    private async Task HandleEditClickAsync() => await RaiseEdit.InvokeAsync(Comment);
}