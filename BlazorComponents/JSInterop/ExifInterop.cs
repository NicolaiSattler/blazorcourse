using Microsoft.JSInterop;

namespace BlazorComponents.JSInterop;

public class ExifInterop
{
    private IJSObjectReference? _module;
    private Lazy<Task<IJSObjectReference>> _moduleTask;

    public ExifInterop(IJSRuntime runtime)
    {
        _moduleTask = new(() =>
        {
            return runtime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponents/Components/Edit/EditPhotoComponent.razor.js")
            .AsTask();
        });
    }
    private Action<double, double> _latlongCallback;

    public async Task InvokeExtractLatLong(Action<double, double> latlongCallback) {

        _latlongCallback = latlongCallback;
        _module ??= await _moduleTask.Value;

        var objectReference = DotNetObjectReference.Create(this);

        await _module.InvokeVoidAsync("extractLatLong", objectReference);
    }

    [JSInvokable]
    public void CallBack(double latitude, double longitude)
    {
        _latlongCallback(latitude, longitude);
    }
}