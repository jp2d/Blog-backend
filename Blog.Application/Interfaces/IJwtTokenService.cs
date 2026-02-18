namespace Blog.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId, string role);
    }
}
