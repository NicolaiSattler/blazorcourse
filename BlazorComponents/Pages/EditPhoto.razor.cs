using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Pages;

public partial class EditPhoto: ComponentBase
{
    [Inject]
    public IPhotoService? PhotoService { get; set; }

    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Parameter]
    public Photo? Photo { get; set; }

    [CascadingParameter] //TODO: enforce auth policy
    public Task<AuthenticationState>? AuthenticationState { get; set; }

    [Parameter]
    public int Id { get; set; }

    protected async override Task OnInitializedAsync()
    {
        if (PhotoService != null)
            Photo = await PhotoService.GetByIdAsync(Id);
    }

    private async Task HandleEdit(Photo changedPhoto)
    {
        if (PhotoService != null)
            await PhotoService.UpdateAsync(changedPhoto);

        NavigationManager?.NavigateTo("/all-photos");
    }
}