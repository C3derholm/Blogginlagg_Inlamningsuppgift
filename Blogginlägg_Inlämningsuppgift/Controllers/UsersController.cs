using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blogginlägg_Inlämningsuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
            public async Task <IActionResult> Login([FromBody] LoginDTO dto)
        {
            var userId = await _userService.LoginAsync(dto);
            
            if (userId == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(new { userId });
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterUserDTO dto)
        {
            try
            {
                var userId = await _userService.RegisterAsync(dto);
                return Ok(new { userId });

            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{userId}")]

        public async Task<IActionResult> GetById(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


    }
}
