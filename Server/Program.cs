using Duende.Bff.Yarp;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddReverseProxy()
                .AddTransforms<AccessTokenTransformProvider>()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
                .AddBffExtensions();

builder.Services.AddBff();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
    options.DefaultSignOutScheme = "oidc";
})
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-blazor";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";

        options.ClientId = "blazorcource.server";
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.Scope.Add("photoRest");
        options.Scope.Add("commentGrpc");

        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();
app.MapRazorPages();
app.MapBffReverseProxy();
app.MapFallbackToFile("index.html");

app.Run();
