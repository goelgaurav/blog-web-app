using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // Full blog body (HTML/markdown)
        public string? Author { get; set; } //For MVP scope, Auth and users is out of scope, Author is stored as string.
        public string? Description { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Comment> Comments { get; set; } = new();
    }
}
