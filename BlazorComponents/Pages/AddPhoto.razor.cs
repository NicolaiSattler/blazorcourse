using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;


namespace BlazorComponents.Pages;

public partial class AddPhoto: ComponentBase
{
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private IPhotoService? PhotoService { get; set;}

    private void HandlePhotoChanged(Photo newPhoto)
    {
        PhotoService?.AddAsync(newPhoto);

        NavigationManager?.NavigateTo("/all-photos");
    }
}