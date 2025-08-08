using AutoMapper;
using Blog.Application.DTOs;
using Blog.Application.Services.Comments;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/blogposts/{postId:guid}/[Controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper) 
        {
            _commentService = commentService; 
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentResponse>>> GetAllCommentsByPostIdAsync(Guid postId)
        {
            var comments = await _commentService.GetAllByPostIdAsync(postId);
            var response = _mapper.Map<List<CommentResponse>>(comments);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetCommentByIdAsync))]
        public async Task<ActionResult<CommentResponse>> GetCommentByIdAsync(Guid postId, Guid id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment is null)
                return NotFound();
            var response = _mapper.Map<CommentResponse>(comment);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponse>> Create(Guid postId, [FromBody] CommentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _mapper.Map<Comment>(request);
            var created = await _commentService.CreateAsync(postId, comment);
            var response = _mapper.Map<CommentResponse>(created);

            return CreatedAtAction(nameof(GetCommentByIdAsync), new { postId, id = created.Id }, response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CommentResponse>> Delete(Guid postId, Guid id)
        {
            var deleted = await _commentService.DeleteAsync(postId, id);

            if(deleted is null)
                return NotFound();

            return NoContent();


        }

    }
}
