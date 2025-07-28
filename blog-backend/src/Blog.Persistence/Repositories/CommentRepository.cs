using Blog.Application.Abstractions;
using Blog.Domain.Entities;
using Blog.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext _context;

        public CommentRepository(BlogDbContext context) => _context = context;

        public async Task<List<Comment>> GetAllByPostIdAsync(Guid postId)
        {
            return await _context.Comments
                .Where(c => c.BlogPostId == postId)
                .ToListAsync();
        }

        public async Task<Comment> AddAsync(Comment comment)
        {
            var entry = await _context.Comments.AddAsync(comment);
            await SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Comment?> UpdateAsync(Comment comment)
        {
            var existing = await _context.Comments.FindAsync(comment.Id);
            if (existing is null)
                return null;

            _context.Entry(existing).CurrentValues.SetValues(comment);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment is null)
                return null;

            _context.Comments.Remove(comment);
            await SaveChangesAsync();
            return comment;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
