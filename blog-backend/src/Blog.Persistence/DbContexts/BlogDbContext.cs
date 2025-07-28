using Blog.Domain.Entities;
using Blog.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.DbContexts
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Comment> Comments => Set<Comment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BlogPostConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());


            // Use static values for seeding
            var postIds = new[]
            {
                Guid.Parse("eb21c477-f44b-4b4d-8309-c46eb88a8e5e"),
                Guid.Parse("1e2ebd66-7ef8-4898-990d-cab47e92b070"),
                Guid.Parse("d31ad2dd-6bd8-40cf-a105-6bb2837f2262"),
                Guid.Parse("f6c9785d-19f0-4dba-9070-200173ba21a5"),
                Guid.Parse("36d0139d-0163-492a-823f-74dc5aebcbe3"),
                Guid.Parse("077e988e-61d1-49e4-bf5f-0a182dfa05db"),
                Guid.Parse("f8ff5e9d-a55b-4595-8d50-b4292cbfc6ce"),
                Guid.Parse("c7d08df2-3835-45ec-8e10-7952543e4f35"),
                Guid.Parse("0ff63aa1-a57f-4df6-99b4-5902229bdfdf"),
                Guid.Parse("01bf6d3d-1a41-43ff-972c-7adc925fbca7")
            };

            var posts = new List<BlogPost>();
            var comments = new List<Comment>();
            var baseDate = new DateTime(2025, 7, 28, 6, 0, 0, DateTimeKind.Utc);

            for (int i = 0; i < 10; i++)
            {
                posts.Add(new BlogPost
                {
                    Id = postIds[i],
                    Title = $"Seed Post {i + 1}",
                    Content = $"This is the content for post {i + 1}.",
                    Author = $"Author {i + 1}",
                    Description = $"Description for post {i + 1}",
                    CreatedAt = baseDate.AddMinutes(i)
                });

                // Seed 3 comments per post with static IDs and dates
                for (int j = 0; j < 3; j++)
                {
                    comments.Add(new Comment
                    {
                        Id = Guid.Parse($"{i + 1:D2}{j + 1:D2}0000-0000-0000-0000-000000000000"),
                        Content = $"Comment {j + 1} for post {i + 1}",
                        Author = $"Commenter {j + 1}",
                        PostedAt = baseDate.AddMinutes(i * 3 + j),
                        BlogPostId = postIds[i]
                    });
                }
            }
            modelBuilder.Entity<BlogPost>().HasData(posts);
            modelBuilder.Entity<Comment>().HasData(comments);
        }
    }
}
