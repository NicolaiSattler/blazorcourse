
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using MyBlazorCourse.Shared.Model;
using MyBlazorCourse.Shared.Validation;

namespace BlazorComponents.Components.Edit;

public partial class EditPhotoComponent
{
    [Parameter]
    public Photo Photo { get; set; } = new();

    private PhotoValidation photoValidation = new();
    private MudForm? form;

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
        if (form != null)
        { 
            await form.Validate();

            if (form.IsValid)
            {
                await PhotoChanged.InvokeAsync(Photo);
            }
        }
    }
}
