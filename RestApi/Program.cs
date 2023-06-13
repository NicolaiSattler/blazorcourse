using Microsoft.EntityFrameworkCore;
using RestApi.DataAccess;
using RestApi.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddDbContext<PhotoDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PhotoDb");
    options.UseSqlite(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
