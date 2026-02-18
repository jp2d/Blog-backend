using Blog.Domain.Enum;

namespace Blog.Api.DTO
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
    }
}
