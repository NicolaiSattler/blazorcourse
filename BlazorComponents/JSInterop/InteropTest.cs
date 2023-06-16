using Microsoft.JSInterop;

namespace BlazorComponents.JSInterop;

public class InteropTest
{
    private IJSObjectReference? _module;
    private Lazy<Task<IJSObjectReference>> _moduleTask;

    public InteropTest(IJSRuntime runtime)
    {
        _moduleTask = new(() =>
        {
            return runtime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponents/Pages/SayHi.js")
            .AsTask();
        });
    }

    public async Task SayHi()
    {
        _module ??= await _moduleTask.Value;
        await _module.InvokeVoidAsync("sayHi");
    }
}