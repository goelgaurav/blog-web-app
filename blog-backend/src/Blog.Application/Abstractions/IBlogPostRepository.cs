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
        Task<BlogPost?> GetByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task AddAsync(BlogPost post);
        Task UpdateAsync(BlogPost post);
        Task DeleteAsync(BlogPost post);
        Task SaveChangesAsync();
    }
}
