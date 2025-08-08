using Blog.Application.Abstractions;
using Blog.Application.Validation;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Services.BlogPosts
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IBlogPostRepository _repository;
        private readonly BlogPostValidator _validator;
        public BlogPostService(IBlogPostRepository repository, BlogPostValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<List<BlogPost>> GetAllAsync(string? search = null)
        {
            return await _repository.GetAllAsync(search);
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<BlogPost> CreateAsync(BlogPost post)
        {
            var validationResult = _validator.Validate(post);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage)));
            post.Id = Guid.NewGuid();
            post.CreatedAt = DateTime.UtcNow;
            return await _repository.AddAsync(post);
        }

        public async Task<BlogPost?> UpdateAsync(Guid id, BlogPost post)
        {
            var validationResult = _validator.Validate(post);
            if (!validationResult.IsValid)
                throw new ArgumentException(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

            var existing = await _repository.GetByIdAsync(id);
            
            if (existing is null) 
                return null;

            existing.Title = post.Title;
            existing.Content = post.Content;
            existing.Description = post.Description;
            existing.Author = post.Author;

            return await _repository.UpdateAsync(existing);
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

