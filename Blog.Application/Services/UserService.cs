using Blog.Application.Interfaces;
using Blog.Domain.Entity;
using Blog.Domain.Interfaces;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<User> CreateUser(User user) => await _userRepository.AddAsync(user);

        public async Task DeleteUser(int id) => await _userRepository.DeleteAsync(id);

        public async Task<IEnumerable<User?>> GetAllUsers() => await _userRepository.GetAllAsync();

        public async Task<User?> GetUserById(int id) => await _userRepository.GetByIdAsync(id);

        public async Task<User> UpdateUser(User user) => await _userRepository.UpdateAsync(user);
    }
}
