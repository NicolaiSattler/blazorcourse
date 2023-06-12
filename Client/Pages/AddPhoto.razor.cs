using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Pages;

public partial class AddPhoto: ComponentBase
{
    [Inject]
    private NavigationManager? NavigationManager { get; set; }
    [Inject]
    private IPhotoService? PhotoService { get; set;}

    public Photo NewPhoto { get; set; } = new();

    public async Task HandleInputFileChanged(IBrowserFile file)
    {
        NewPhoto.Data = new byte[file.Size];

        await file.OpenReadStream().ReadAsync(NewPhoto.Data);

        NewPhoto.ImageMimetype = file.ContentType;

        Console.WriteLine($"{NewPhoto.ImageMimetype}");
    }

    public void HandleSubmit()
    {
        Console.WriteLine($"Title: {NewPhoto.Title} Description: {NewPhoto.Description}");

        PhotoService?.AddAsync(NewPhoto);
        NavigationManager?.NavigateTo("/all-photos");
    }
}