using Blog.Domain.Entity;
using Blog.Domain.Interfaces;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context) => _context = context;

        public async Task<Post> AddAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post is not null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllAsync() => await _context.Posts
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        public async Task<IEnumerable<Post>> GetAllByAuthorIdAsync(int id) => await _context.Posts
             .Where(p => p.User.Id == id)
             .ToListAsync();

        public async Task<Post?> GetByIdAsync(int id) => await _context.Posts.FindAsync(id);

        public async Task<Post> UpdateAsync(Post post)
        {
            var existingPost = await _context.Posts.FindAsync(post.Id);
            if (existingPost == null)
                throw new Exception("Post não encontrado");
                        
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.UpdatedAt = DateTime.UtcNow;

            _context.Posts.Update(existingPost);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
