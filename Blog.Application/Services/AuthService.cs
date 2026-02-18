using Blog.Application.Interfaces;
using Blog.Domain.Interfaces;

namespace Blog.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }
        public async Task<string> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is null)
                return string.Empty;

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return string.Empty;

            return _jwtTokenService.GenerateToken(user.Id.ToString(), user.Role.ToString())
                ?? throw new ArgumentNullException(nameof(_jwtTokenService));
        }
    }
}
