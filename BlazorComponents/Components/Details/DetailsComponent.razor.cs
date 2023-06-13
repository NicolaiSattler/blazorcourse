using Microsoft.AspNetCore.Components;
using MyBlazorCourse.Shared.Model;

namespace BlazorComponents.Components.Details;

public partial class DetailsComponent: ComponentBase
{
    [Parameter]
    public Photo? Photo { get; set;}
}
