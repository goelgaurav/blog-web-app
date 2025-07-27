using Blog.Application.Services.BlogPosts;
using Blog.Application.Validation;
using Blog.Domain.Entities;
using Blog.Application.Abstractions;
using Moq;
using Xunit;

namespace Blog.Tests.Services
{
    public class BlogPostServiceTests
    {
        private readonly Mock<IBlogPostRepository> _repoMock;
        private readonly BlogPostValidator _validator;
        private readonly BlogPostService _service;

        public BlogPostServiceTests()
        {
            _repoMock = new Mock<IBlogPostRepository>();
            _validator = new BlogPostValidator();
            _service = new BlogPostService(_repoMock.Object, _validator);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsList()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAllAsync())
                     .ReturnsAsync(new List<BlogPost> { new() { Title = "Test" } });

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            Assert.Equal("Test", result.First().Title);
        }

        [Fact]
        public async Task CreateAsync_Throws_When_Invalid()
        {
            // Arrange
            var post = new BlogPost { Title = "" }; // invalid

            // Act + Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.CreateAsync(post));

            Assert.Contains("Title", ex.Message);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsNull_When_NotFound()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>()))
                     .ReturnsAsync((BlogPost?)null);

            // Act
            var result = await _service.DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsNull_When_NotFound()
        {
            // Arrange
            var updated = new BlogPost { Title = "Updated Title", Content = "New" };

            _repoMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                     .ReturnsAsync((BlogPost?)null); // simulate not found

            // Act
            var result = await _service.UpdateAsync(Guid.NewGuid(), updated);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_Throws_When_Invalid()
        {
            // Arrange
            var post = new BlogPost { Title = "" }; // invalid input

            // Act + Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                _service.UpdateAsync(Guid.NewGuid(), post));

            Assert.Contains("Title", ex.Message);
        }
    }
}
