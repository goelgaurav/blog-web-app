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
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogDbContext _context;

        public BlogPostRepository(BlogDbContext context) => _context = context;

        public async Task<BlogPost?> GetByIdAsync(int id) =>
            await _context.BlogPosts.FindAsync(id);

        public async Task<IEnumerable<BlogPost>> GetAllAsync() =>
            await _context.BlogPosts.Include(p => p.Comments).ToListAsync();

        public async Task AddAsync(BlogPost post) => await _context.BlogPosts.AddAsync(post);

        public Task UpdateAsync(BlogPost post)
        {
            _context.BlogPosts.Update(post);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(BlogPost post)
        {
            _context.BlogPosts.Remove(post);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
