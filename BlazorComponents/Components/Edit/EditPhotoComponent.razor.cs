
using BlazorComponents.JSInterop;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using MyBlazorCourse.Shared.Model;
using MyBlazorCourse.Shared.Validation;

namespace BlazorComponents.Components.Edit;

public partial class EditPhotoComponent
{
    private PhotoValidation photoValidation = new();
    private MudForm? form;

    [Parameter]
    public Photo Photo { get; set; } = new();

    [Parameter]
    public EventCallback<Photo> PhotoChanged { get; set; }

    [Inject]
    private LeafletInterop? LeafletInterop { get; set;}

    [Inject]
    private ExifInterop? ExifInterop { get; set; }

    private void RetrieveLatLong(double latitude, double longitude)
    {
        Console.WriteLine($"lat: {latitude} long: {longitude}");
    }

    public async Task HandleInputFileChanged(IBrowserFile file)
    {
        Photo.Data = new byte[file.Size];

        await file.OpenReadStream().ReadAsync(Photo.Data);

        Photo.ImageMimetype = file.ContentType;
    }

    public async Task HandleSubmit()
    {
        if (form != null)
        {
            await form.Validate();

            if (form.IsValid)
            {
                await ExifInterop.InvokeExtractLatLong(RetrieveLatLong);
                await PhotoChanged.InvokeAsync(Photo);
            }
        }
    }

}
