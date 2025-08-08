using Blog.Application.Abstractions;
using Blog.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Tests.TestUtils
{
    public class MockRepositoryFactory
    {
        public static Mock<IBlogPostRepository> Create()
        {
            var mockRepo = new Mock<IBlogPostRepository>();

            mockRepo.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string?>()))
                .ReturnsAsync(new List<BlogPost> {
                        new() { Id = Guid.NewGuid(), Title = "Test 1", Content = "Body" },
                        new() { Id = Guid.NewGuid(), Title = "Test 2", Content = "Body" }
                });

            mockRepo.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new BlogPost { Id = id, Title = "Test", Content = "Body" });

            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<BlogPost>()))
                .ReturnsAsync((BlogPost post) => post);

            mockRepo.Setup(repo => repo.UpdateAsync(It.IsAny<BlogPost>()))
                .ReturnsAsync((BlogPost post) => post);

            mockRepo.Setup(repo => repo.DeleteAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new BlogPost { Id = id });

            return mockRepo;
        }
    }
}
