using Blog.Application.Abstractions;
using Blog.Domain.Entities;
using Blog.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext _context;

        public CommentRepository(BlogDbContext context) => _context = context;

        public async Task<IEnumerable<Comment>> GetAllByPostIdAsync(int postId)
        {
            return await _context.Comments
                    .Where(c => c.BlogPostId == postId)
                    .ToListAsync();
        }

        public async Task AddAsync(Comment comment) => await _context.Comments.AddAsync(comment);

        public Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
