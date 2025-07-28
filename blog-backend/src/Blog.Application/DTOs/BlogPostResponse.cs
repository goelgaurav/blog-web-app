using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs
{
    public class BlogPostResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CommentResponse> Comments { get; set; } = new();
        public int CommentCount { get; set; }
    }

}
