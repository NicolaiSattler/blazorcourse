using Microsoft.EntityFrameworkCore;
using MyBlazorCourse.Shared.Model;
using Infrastructure.DataAccess;

namespace Infrastructure.Repository;

public interface IPhotoRepository
{
    Task<Photo> AddAsync(Photo newPhoto);
    Task<IEnumerable<Photo>> GetAsync();
    Task<Photo?> GetByIdAsync(int id);
    Task<int> RemoveAsync(int id);
    Task<Photo?> UpdateAsync(Photo changedPhoto);
}

public class PhotoRepository : IPhotoRepository
{
    private readonly PhotoDbContext _context;

    public PhotoRepository(PhotoDbContext context)
    {
        _context = context;
    }

    public async Task<Photo> AddAsync(Photo newPhoto)
    {
       _context.Photos.Add(newPhoto);

        await _context.SaveChangesAsync();

        return newPhoto;
    }
    public async Task<IEnumerable<Photo>> GetAsync() => await _context.Photos.ToListAsync();

    public async Task<Photo?> GetByIdAsync(int id) => await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Photo?> UpdateAsync(Photo changedPhoto)
    {
        _context.Photos.Update(changedPhoto);

        await _context.SaveChangesAsync();

        return changedPhoto;
    }

    public async Task<int> RemoveAsync(int id)
    {
        var item = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

        if (item is not null)
        {
            var result = _context.Remove(item);

            await _context.SaveChangesAsync();

            return id;
        }

        return default;
    }
}