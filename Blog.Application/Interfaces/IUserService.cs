using Blog.Domain.Entity;

namespace Blog.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetUserById(int id);
        Task<IEnumerable<User?>> GetAllUsers();
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
