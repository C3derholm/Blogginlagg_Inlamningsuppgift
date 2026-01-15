using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Bloginlägg_Inlämningsuppgift.Controllers
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

        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreatePostDTO dto)
        {
            try
            {
                var postId = await _postService.CreateAsync(dto);
                return Ok(new { postId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]

        public async Task<IActionResult> GettAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

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

        [HttpPut("{postId:int}")]

        public async Task<IActionResult> Update(int postId, [FromQuery] int userId, [FromBody] UpdatePostDTO dto)

        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID");
            }

            try
            {
                var updated = await _postService.UpdateAsync(postId, userId, dto);
                if (!updated) return NotFound();
                return NoContent();
            }

            catch (InvalidOperationException ex)
            {
                return Forbid();
            }




        }

        [HttpDelete("{postId:int}")]

        public async Task<IActionResult> Delete(int postId, [FromQuery] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID");
            }
            try
            {
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
