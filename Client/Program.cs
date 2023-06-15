using Grpc.Net.Client.Web;
using GrpcService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MyBlazorCourse.Client;
using MyBlazorCourse.Client.Service;
using MyBlazorCourse.Shared.Interface;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddGrpcClient<Commenter.CommenterClient>(options =>
{
    options.Address = new Uri(builder.HostEnvironment.BaseAddress);
}).ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

await builder.Build().RunAsync();
