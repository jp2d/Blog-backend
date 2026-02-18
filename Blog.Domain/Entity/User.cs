using Blog.Domain.Enum;

namespace Blog.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string PasswordHash { get; set; }
        public Roles Role { get; set; } = Roles.User;
        public ICollection<Post>? Posts { get; set; }
    }
}
