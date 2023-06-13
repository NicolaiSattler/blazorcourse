using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Pages;

public partial class PhotoDetails: ComponentBase
{
    [Inject]
    public IPhotoService? PhotoService { get; set;}

    [Parameter]
    public int Id { get; set; }

    private Photo? Model { get; set; }

    protected async override Task OnInitializedAsync()
    {
        if (PhotoService is not null)
        {
            Model = await PhotoService.GetByIdAsync(Id);
            Console.WriteLine(Model.Id);
            Console.WriteLine(Model == null);

        }
    }
}