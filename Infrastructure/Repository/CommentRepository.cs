using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using MyBlazorCourse.Shared.Model;

namespace Infrastructure.Repository;

public interface ICommentRepository
{
    Task<Comment> AddAsync(Comment newComment);
    Task<IList<Comment>?> GetByPhotoIdAsync(int photoId);
    Task<Comment?> RemoveAsync(int id);
    Task<Comment?> UpdateAsync(Comment changedComment);
}

public class CommentRepository : ICommentRepository
{
    private readonly PhotoDbContext _context;

    public CommentRepository(PhotoDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Comment> AddAsync(Comment newComment)
    {
        var result = _context.Add(newComment);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<IList<Comment>?> GetByPhotoIdAsync(int id) => await _context.Comments.Where(p => p.Id == id)
                                                                                           .ToListAsync();

    public async Task<Comment?> UpdateAsync(Comment changedComment)
    {
        var result = _context.Update(changedComment);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Comment?> RemoveAsync(int id)
    {
        var item = await _context.Comments.FirstOrDefaultAsync(p => p.Id == id);

        if (item is not null)
        {
            var result = _context.Remove(item);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        return default;
    }
}