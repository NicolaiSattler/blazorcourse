using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Pages;

public partial class DeletePhoto: ComponentBase
{
    [Inject]
    private IPhotoService? PhotoService { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Parameter]
    public int Id { get; set; }

    private Photo? Model { get; set; }

    private async Task HandleDeleteClick()
    {
        if (PhotoService != null)
            await PhotoService.DeleteAsync(Id);

        NavigationManager?.NavigateTo("/all-photos");
    }

    protected async override Task OnInitializedAsync()
    {
        if (PhotoService != null)
            Model = await PhotoService.GetByIdAsync(Id);
    }
}