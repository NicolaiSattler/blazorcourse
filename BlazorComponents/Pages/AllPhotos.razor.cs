using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Pages;

public partial class AllPhotos: ComponentBase
{
    [Inject]
    public IPhotoService? PhotoService { get; set;}

    public ICollection<Photo> PhotoCollection { get; set; } = new List<Photo>();

    protected override async Task OnInitializedAsync()
    {
        if (PhotoService != null)
        {
            PhotoCollection = await PhotoService.GetAllAsync();
        }
    }
}