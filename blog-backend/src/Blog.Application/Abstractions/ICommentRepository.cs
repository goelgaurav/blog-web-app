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
        Task<List<Comment>> GetAllByPostIdAsync(Guid postId);
        Task<Comment?> GetCommentByIdAsync(Guid Id);
        Task<Comment> AddAsync(Comment comment);
        Task<Comment?> UpdateAsync(Comment comment);
        Task<Comment?> DeleteAsync(Guid postId, Guid id);
        Task SaveChangesAsync();
    }
}
