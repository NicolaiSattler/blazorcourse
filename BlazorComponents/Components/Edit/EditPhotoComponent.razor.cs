
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Components.Edit;

public partial class EditPhotoComponent: ComponentBase
{
    [Inject]
    private IPhotoService? PhotoService { get; set;}

    [Parameter]
    public Photo Photo { get; set; } = new();

    [Parameter]
    public EventCallback<Photo> PhotoChanged { get; set; }

    public async Task HandleInputFileChanged(IBrowserFile file)
    {
        Photo.Data = new byte[file.Size];

        await file.OpenReadStream().ReadAsync(Photo.Data);

        Photo.ImageMimetype = file.ContentType;
    }

    public async Task HandleSubmit()
    {
        await PhotoChanged.InvokeAsync(Photo);
    }
}
