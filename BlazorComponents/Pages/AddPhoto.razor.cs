using BlazorComponents.JSInterop;
using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;


namespace BlazorComponents.Pages;

public partial class AddPhoto: ComponentBase
{
    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private IPhotoService? PhotoService { get; set; }

    private async Task HandlePhotoChanged(Photo newPhoto)
    {
        try
        {
            if (PhotoService != null)
            {
                await PhotoService.AddAsync(newPhoto);
            }

            NavigationManager?.NavigateTo("/all-photos");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}