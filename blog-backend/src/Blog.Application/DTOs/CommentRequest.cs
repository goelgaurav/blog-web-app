using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.DTOs
{
    public class CommentRequest
    {
        public string Content { get; set; } = string.Empty;
        public string? Author { get; set; }

    }
}
