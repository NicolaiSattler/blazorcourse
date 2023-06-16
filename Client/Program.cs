using BlazorComponents.JSInterop;
using Grpc.Net.Client.Web;
using GrpcService;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyBlazorCourse.Client;
using MyBlazorCourse.Client.Auth;
using MyBlazorCourse.Client.Service;
using MyBlazorCourse.Shared.Interface;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();

builder.Services.AddMudServices();

builder.Services.AddScoped<InteropTest>();
builder.Services.AddScoped<LeafletInterop>();
builder.Services.AddScoped<ExifInterop>();

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddTransient<AntiforgeryHandler>();
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("backend"));

builder.Services.AddHttpClient("backend", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<AntiforgeryHandler>();

builder.Services.AddGrpcClient<Commenter.CommenterClient>(options =>
{
    options.Address = new Uri(builder.HostEnvironment.BaseAddress);
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new AntiforgeryHandler(new HttpClientHandler())));

await builder.Build().RunAsync();
