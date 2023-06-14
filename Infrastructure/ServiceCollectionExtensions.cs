using Infrastructure.DataAccess;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IPhotoRepository, PhotoRepository>();
        services.AddDbContext<PhotoDbContext>(options =>
        {
            var connectionString = config.GetConnectionString("PhotoDb");
            options.UseSqlite(connectionString);
        });
        return services;
    }
}