using Blog.Application.Services.BlogPosts;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BlogPostsController : ControllerBase
    {
        //ToDo: implement and use DTOs for request/response models

        private readonly IBlogPostService _blogPostService;
        public BlogPostsController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogPost>>> GetAllPostsAsync()
        {
            var posts = await _blogPostService.GetAllAsync();

            return Ok(posts);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BlogPost>> GetPostByIdAsync(Guid id)
        {
            var post = await _blogPostService.GetByIdAsync(id);
            if (post is null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> CreatePostAsync([FromBody] BlogPost post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _blogPostService.CreateAsync(post);
            return CreatedAtAction(nameof(GetPostByIdAsync), new { id = created.Id, created });
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<BlogPost>> UpdatePostAsync(Guid id, [FromBody] BlogPost post)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _blogPostService.UpdateAsync(id, post);
            if (updated is null)
                return BadRequest();

            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<BlogPost>> DeleteByIdAsync(Guid id)
        {
            var deleted = await _blogPostService.DeleteAsync(id);
            if (deleted is null)
                return NotFound();

            return NoContent();
        }
    }
}
