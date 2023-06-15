using Infrastructure;
using Microsoft.IdentityModel.Tokens;
using MyBlazorCourse.Shared.Authorization;
using RestApi.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
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

builder.Services.AddScoped<RestApi.Service.IPhotoService, PhotoService>();
builder.Services.AddSingleton<UpdatePhotoAuthorizationHandler>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UpdatePhoto", policy => policy.Requirements.Add(new UpdatePhotoAuthorizationRequirement()));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
