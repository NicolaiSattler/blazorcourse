using BlazorComponents.JSInterop;
using Microsoft.AspNetCore.Components;

namespace BlazorComponents.Components.Map;

public partial class MapComponent: ComponentBase
{
    [Inject]
    private LeafletInterop? Interop { get; set;}

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (Interop != null)
            await Interop.ShowMap("test");
    }
}