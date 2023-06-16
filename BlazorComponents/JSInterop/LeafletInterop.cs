using Microsoft.JSInterop;

namespace BlazorComponents.JSInterop;

public class LeafletInterop
{
    private IJSObjectReference? _module;
    private Lazy<Task<IJSObjectReference>> _moduleTask;

    public LeafletInterop(IJSRuntime runtime)
    {
        _moduleTask = new(() =>
        {
            return runtime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponents/Components/Map/MapComponent.razor.js")
            .AsTask();
        });
    }

    public async Task ShowMap(string title)
    {
        _module ??= await _moduleTask.Value;
        await _module.InvokeVoidAsync("showMap", title);
    }
}