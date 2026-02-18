using Blog.Domain.Entity;

namespace Blog.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post?>> GetAllPosts();
        Task<IEnumerable<Post?>> GetAllPostsByAuthorId(int id);
        Task<Post?> GetPostById(int id);
        Task<Post> CreatePost(Post post);
        Task<Post> UpdatePost(Post post);
        Task DeletePost(int id);
    }
}
