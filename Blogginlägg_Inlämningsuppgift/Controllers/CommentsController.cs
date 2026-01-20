using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// Stavning korrigerad: "Bloginlägg" -> "Blogginlägg"
namespace Blogginlägg_Inlämningsuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [Authorize]
        [HttpPost]     
        public async Task<IActionResult> Create([FromBody] CreateCommentDTO dto)
        {
            try
            {
                var commentId = await _commentService.CreateAsync(dto);
                return Ok(new { commentId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("post/{postId:int}")]

        public async Task<IActionResult> GetByPostId(int postId)
        {
            var comments = await _commentService.GetByPostIdAsync(postId);
            return Ok(comments);
        }

        [Authorize]
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            try
            {
                // Hämta användarens ID från JWT-token
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                var deleted = await _commentService.DeleteAsync(commentId, userId);
                if (!deleted)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Forbid();
            }
        }






    }
}
