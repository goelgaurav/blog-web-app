using Blog.Application.Abstractions;
using Blog.Domain.Entities;
using Blog.Application.Validation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Application.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly CommentValidator _validator;

        public CommentService(ICommentRepository repository, CommentValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<List<Comment>> GetAllByPostIdAsync(Guid postId)
        {
            return await _repository.GetAllByPostIdAsync(postId);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            var validationResult = _validator.Validate(comment);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage)));

            comment.PostedAt = System.DateTime.UtcNow;
            return await _repository.AddAsync(comment);
        }

        public async Task<Comment?> UpdateAsync(Guid id, Comment comment)
        {
            var validationResult = _validator.Validate(comment);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage)));

            var existing = await _repository.DeleteAsync(id);
            if (existing is null)
                return null;

            existing.Content = comment.Content;
            existing.Author = comment.Author;
            // Optionally update PostedAt or other fields

            return await _repository.UpdateAsync(existing);
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}