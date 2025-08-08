using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.BlogPosts
{
    public interface IBlogPostService
    {
        Task<List<BlogPost>> GetAllAsync(string? search = null);
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<BlogPost> CreateAsync(BlogPost post);
        Task<BlogPost?> UpdateAsync(Guid id, BlogPost post);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}
