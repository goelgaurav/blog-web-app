using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence.Configurations
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(150);
            builder.Property(p => p.Description).HasMaxLength(500);

            builder.HasMany(p => p.Comments)
                   .WithOne(c => c.BlogPost!)
                   .HasForeignKey(c => c.BlogPostId);
        }
    }
}
