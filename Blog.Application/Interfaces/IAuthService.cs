using Blog.Domain.Entity;

namespace Blog.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(string email, string password);
        Task<User?> GetUserByEmail(string email);
    }
}
