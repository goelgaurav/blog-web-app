using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? Author { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.UtcNow;

        public Guid BlogPostId { get; set; }
        public BlogPost? BlogPost { get; set; }
    }
}
