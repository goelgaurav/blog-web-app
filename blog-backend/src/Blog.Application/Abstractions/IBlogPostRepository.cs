using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions
{
    public interface IBlogPostRepository
    {
        Task<BlogPost?> GetByIdAsync(Guid id);
        Task<List<BlogPost>> GetAllAsync(int page, int pageSize, string sort, string? search = null);
        Task<BlogPost> AddAsync(BlogPost post);
        Task<BlogPost?> UpdateAsync(BlogPost post);
        Task<BlogPost?> DeleteAsync(Guid id);
        Task SaveChangesAsync();
    }
}
