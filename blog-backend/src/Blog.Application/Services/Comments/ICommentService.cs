using Blog.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Application.Services.Comments
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllByPostIdAsync(Guid postId);
        Task<Comment?> GetCommentByIdAsync(Guid Id);
        Task<Comment> CreateAsync(Guid PostId, Comment comment);
        Task<Comment?> UpdateAsync(Guid id, Comment comment);
        Task<Comment?> DeleteAsync(Guid id);
    }
}