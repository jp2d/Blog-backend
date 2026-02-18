using Blog.Api.DTO;
using Blog.Application.Interfaces;
using Blog.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService) => _postService = postService;

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("GetAllPostsByAuthorId")]
        [Authorize]
        public async Task<IActionResult> GetAllPostsByAuthorId(int id)
        {
            var _user = HttpContext.User;

            if (_user.Identity is not { IsAuthenticated: true })
                return Unauthorized();

            var posts = await _postService.GetAllPostsByAuthorId(id);
            return Ok(posts);
        }

        [HttpGet("GetPostById/{id}")]
        [Authorize]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostById(id);
            return post is not null ? Ok(post) : NotFound();
        }

        [HttpPost("CreatePost")]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto dto)
        {
            var post = new Post
            {
                Titule = dto.Titule,
                Content = dto.Content,
                UserId = dto.UserId
            };

            var createdPost = await _postService.CreatePost(post);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("UpdatePost")]
        [Authorize]
        public async Task<IActionResult> UpdatePost([FromBody] PostDto postDto)
        {
            var post = new Post
            {
                Titule = postDto.Titule,
                Content = postDto.Content
            };

            var updatedPost = await _postService.UpdatePost(post);
            return Ok(updatedPost);
        }

        [HttpDelete("DeletePost/{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePost(id);
            return NoContent();
        }
    }
}
