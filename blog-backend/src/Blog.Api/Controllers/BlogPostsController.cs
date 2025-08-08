using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.Services.BlogPosts;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IMapper _mapper; 
        public BlogPostsController(IBlogPostService blogPostService, IMapper mapper)
        {
            _blogPostService = blogPostService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogPostResponse>>> GetAllPostsAsync([FromQuery] string? search = null)
        {
            var posts = await _blogPostService.GetAllAsync(search);
            var response = _mapper.Map<List<BlogPostResponse>>(posts);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetPostByIdAsync))]
        public async Task<ActionResult<BlogPostResponse>> GetPostByIdAsync(Guid id)
        {
            var post = await _blogPostService.GetByIdAsync(id);
            if (post is null)
                return NotFound();
            var response = _mapper.Map<BlogPostResponse>(post);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPostResponse>> CreatePostAsync([FromBody] BlogPostRequest postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var post = _mapper.Map<BlogPost>(postDto);

            var created = await _blogPostService.CreateAsync(post);

            var response = _mapper.Map<BlogPostResponse>(created);

            return CreatedAtAction(nameof(GetPostByIdAsync), new { id = created.Id }, response );
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<BlogPostResponse>> UpdatePostAsync(Guid id, [FromBody] BlogPostRequest postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var post = _mapper.Map<BlogPost>(postDto);
            var updated = await _blogPostService.UpdateAsync(id, post);
            
            if (updated is null)
                return BadRequest();
            var response = _mapper.Map<BlogPostResponse>(updated);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<BlogPostResponse>> DeleteByIdAsync(Guid id)
        {
            var deleted = await _blogPostService.DeleteAsync(id);
            if (deleted is null)
                return NotFound();

            return NoContent();
        }
    }
}
