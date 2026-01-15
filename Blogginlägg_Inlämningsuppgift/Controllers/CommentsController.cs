using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloginlägg_Inlämningsuppgift.Controllers
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

        [HttpGet("post/{postId:int}")]

        public async Task<IActionResult> GetByPostId(int postId)
        {
            var comments = await _commentService.GetByPostIdAsync(postId);
            return Ok(comments);
        }

        [HttpDelete("{commentId:int}")]

        public async Task<IActionResult> Delete(int commentId, [FromQuery] int userId)
        {
            try
            {
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
