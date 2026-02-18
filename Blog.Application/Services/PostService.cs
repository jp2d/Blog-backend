using Blog.Application.Interfaces;
using Blog.Domain.Entity;
using Blog.Domain.Interfaces;

namespace Blog.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository) => _postRepository = postRepository;

        public async Task<Post> CreatePost(Post post) => await _postRepository.AddAsync(post);

        public async Task DeletePost(int id) => await _postRepository.DeleteAsync(id);

        public async Task<IEnumerable<Post?>> GetAllPosts() => await _postRepository.GetAllAsync();

        public async Task<IEnumerable<Post?>> GetAllPostsByAuthorId(int id) => await _postRepository.GetAllByAuthorIdAsync(id);

        public Task<Post?> GetPostById(int id) => _postRepository.GetByIdAsync(id);

        public async Task<Post> UpdatePost(Post post) => await _postRepository.UpdateAsync(post);
    }
}
