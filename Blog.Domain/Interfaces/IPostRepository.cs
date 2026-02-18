using Blog.Domain.Entity;

namespace Blog.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> GetAllByAuthorIdAsync(int id);
        Task<Post?> GetByIdAsync(int id);
        Task<Post> AddAsync(Post post);
        Task<Post> UpdateAsync(Post post);
        Task DeleteAsync(int id);
    }
}
