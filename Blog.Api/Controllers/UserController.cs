using Blog.Api.DTO;
using Blog.Application.Interfaces;
using Blog.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            if (users is null || !users.Any())
            {
                return NotFound();
            }

            var userDtos = users
                .Where(u => u is not null)
                .Select(u => new UserDTO
                {
                    Id = u!.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role,
                    Posts = u.Posts?.Select(p => new PostDto
                    {
                        id = p.Id,
                        Titule = p.Titule,
                        Content = p.Content,
                        CreatedAt = p.CreatedAt
                    }).ToList()
                })
                .ToList();

            return Ok(userDtos);
        }

        [HttpGet("GetUserById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user is null)
            {
                return NotFound();
            }

            var userDto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Posts = user.Posts?.Select(p => new PostDto
                {
                    id = p.Id,
                    Titule = p.Titule,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt
                }).ToList()
            };

            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                PasswordHash = dto.Password
            };

            var createdUser = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("UpdateUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto userDto)
        {
            if (userDto is null)
            {
                return BadRequest();
            }
            var user = new User
            {
                Email = userDto.Email,
                Id = userDto.Id,
                Name = userDto.Name,
                Role = userDto.Role
            };

            var updatedUser = await _userService.UpdateUser(user);
            return Ok(updatedUser);
        }

        [HttpDelete("DeleteUser/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
