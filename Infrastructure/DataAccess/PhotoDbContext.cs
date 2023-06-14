using Microsoft.EntityFrameworkCore;
using MyBlazorCourse.Shared.Model;

namespace Infrastructure.DataAccess;

public class PhotoDbContext: DbContext
{
    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<Comment> Comments => Set<Comment>();

    public PhotoDbContext() { }
    public PhotoDbContext(DbContextOptions<PhotoDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PhotoDbContext).Assembly);
    }
}