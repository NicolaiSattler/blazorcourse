using System.Runtime.InteropServices;
using GrpcService.Services;
using Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var isOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

if (builder.Environment.IsDevelopment() && isOSX)
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.Limits.MinRequestBodyDataRate = null;
        options.ListenLocalhost(5105, o => o.Protocols = HttpProtocols.Http1);
        options.ListenLocalhost(7269, o => o.Protocols = HttpProtocols.Http2);
    });
}
builder.Services.AddGrpc();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        NameClaimType = "name"
                    };
                });

var app = builder.Build();


app.MapGrpcService<CommenterService>().EnableGrpcWeb();
app.UseAuthentication();
app.UseAuthorization();
app.UseGrpcWeb();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
