using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Blogginlägg_Inlämningsuppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]        
            public async Task <IActionResult> Login([FromBody] LoginDTO dto)
        {
            var user = await _userService.LoginAsync(dto);
            
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token =_tokenService.CreateToken(user.UserID, user.Username);
            return Ok(new { token });
        }

        [AllowAnonymous]
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

        [Authorize]
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

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO dto)
        {
            try
            {
                // Hämta användarens ID från JWT-token
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                await _userService.UpdateAsync(userId, dto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                // Hämta användarens ID från JWT-token
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                await _userService.DeleteAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
