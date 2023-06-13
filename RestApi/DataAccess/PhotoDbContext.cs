using Microsoft.EntityFrameworkCore;
using MyBlazorCourse.Shared.Model;

namespace RestApi.DataAccess;

public class PhotoDbContext: DbContext
{
    public DbSet<Photo> Photos => Set<Photo>();

    public PhotoDbContext() { }
    public PhotoDbContext(DbContextOptions<PhotoDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhotoDbContext).Assembly);
    }
}