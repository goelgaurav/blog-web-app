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

        public async Task<BlogPost?> GetByIdAsync(Guid id) =>
            await _context.BlogPosts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<BlogPost>> GetAllAsync(string? search = null)
        {
            var query = _context.BlogPosts.Include(p => p.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(search) && search.Length >= 2)
                query = query.Where(p => p.Title.Contains(search));
            return await query.ToListAsync();
        }

        public async Task<BlogPost> AddAsync(BlogPost post)
        {
            var entry = await _context.BlogPosts.AddAsync(post);
            await SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost post)
        {
            var existing = await _context.BlogPosts.FindAsync(post.Id);
            if (existing is null)
                return null;
            _context.Entry(existing).CurrentValues.SetValues(post);
            await SaveChangesAsync();
            return existing;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var post = await _context.BlogPosts.FindAsync(id);
            if(post is null)
                return null;

            _context.BlogPosts.Remove(post);
            await SaveChangesAsync();
            return post;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
