using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;


namespace BlazorComponents.Pages;

public partial class AddPhoto: ComponentBase
{
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ILogger<AddPhoto>? Logger { get; set; }

    [Inject]
    private IPhotoService? PhotoService { get; set;}

    private async Task HandlePhotoChanged(Photo newPhoto)
    {
        if (PhotoService == null)
        {
            var result = await PhotoService?.AddAsync(newPhoto);

            if (result == null)
                Logger?.LogError("Failed to add photo");

        }

        NavigationManager?.NavigateTo("/all-photos");
    }
}