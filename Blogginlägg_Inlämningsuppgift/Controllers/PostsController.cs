using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
namespace Blogginlägg_Inlämningsuppgift.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]        
        public async Task<IActionResult> Create([FromBody] CreatePostDTO dto)
        {
            try
            {
                var userId = int.Parse(
                    User.FindFirstValue(ClaimTypes.NameIdentifier)!
                    );               

                var postId= await _postService.CreateAsync(dto,userId);

                return Ok(new {postId});

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }
        
        [Authorize]
        [HttpGet("{postId:int}")]
        public async Task<IActionResult> GetById(int postId)

        {
            var post = await _postService.GetByIdAsync(postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [AllowAnonymous]
        [HttpGet("Search/title")]
        public async Task<IActionResult> SearchByTitle([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query is required");
            }
            var posts = await _postService.SearchByTitleAsync(query);
            return Ok(posts);
        }
        [AllowAnonymous]
        [HttpGet("search/category")]
        public async Task<IActionResult> SearchByCategory([FromQuery] string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest("Category name is required.");
            }

            var posts = await _postService.SearchByCategoryAsync(categoryName);
            return Ok(posts);
        }
        
        [Authorize]
        [HttpPut("{postId:int}")]
        public async Task<IActionResult> Update(int postId, [FromBody] UpdatePostDTO dto)
        {
            try
            {
               
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var updated = await _postService.UpdateAsync(postId, userId, dto);
                if (!updated) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Forbid();
            }
        }

        [Authorize]
        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> Delete(int postId)
        {
            try
            {
                
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var deleted = await _postService.DeleteAsync(postId, userId);
                if (!deleted) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Forbid();
            }
        }


    }
}
