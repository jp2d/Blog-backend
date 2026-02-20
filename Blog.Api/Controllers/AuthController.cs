using Blog.Api.DTO;
using Blog.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto login)
        {
            var user = await _authService.GetUserByEmail(login.email);
            var _userDto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };

            var token = await _authService.Authenticate(login.email, login.password);

            return !string.IsNullOrEmpty(token) ? Ok(new { token, profile = _userDto }) : Unauthorized();
        }
    }
}
