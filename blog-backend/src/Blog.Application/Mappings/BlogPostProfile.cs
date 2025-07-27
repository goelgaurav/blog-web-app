using AutoMapper;
using Blog.Application.DTOs;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Mappings
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPostRequest, BlogPost>();
            CreateMap<BlogPost, BlogPostResponse>();
        }
    }
}
