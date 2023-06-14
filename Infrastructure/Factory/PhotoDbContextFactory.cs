using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class PhotoSqliteDbContextFactory : IDesignTimeDbContextFactory<PhotoDbContext>
{
    private const string ChessDbName = "ChessDb";

    public PhotoDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationManager().SetBasePath(Directory.GetCurrentDirectory())
                                               .AddJsonFile("./infra-appsettings.json")
                                               .Build();

        var connectionString = config.GetConnectionString(ChessDbName);
        var optionsBuilder = new DbContextOptionsBuilder<PhotoDbContext>();
        optionsBuilder.UseSqlite(connectionString);

        return new(optionsBuilder.Options);
    }
}