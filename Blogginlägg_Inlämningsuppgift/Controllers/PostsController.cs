using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
