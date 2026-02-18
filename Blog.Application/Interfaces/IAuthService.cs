namespace Blog.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Authenticate(string email, string password);
    }
}
