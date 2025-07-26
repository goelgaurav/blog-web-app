using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Abstractions
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllByPostIdAsync(int postId);
        Task AddAsync(Comment comment);
        Task DeleteAsync(Comment comment);
        Task SaveChangesAsync();
    }
}
