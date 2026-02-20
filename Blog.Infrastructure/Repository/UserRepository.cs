using Blog.Domain.Entity;
using Blog.Domain.Interfaces;
using Blog.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogDbContext _context;

        public UserRepository(BlogDbContext context) => _context = context;

        public async Task<User> AddAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users
            .Include(u => u.Posts)
            .ToListAsync();

        public async Task<User?> GetByIdAsync(int id) => await _context.Users
            .Include(u => u.Posts)
            .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);
            if (existingUser == null)
                throw new Exception("Usuário não encontrado");

            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                user.PasswordHash = existingUser.PasswordHash;
            }
            else if (!BCrypt.Net.BCrypt.Verify(user.PasswordHash, existingUser.PasswordHash))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            }

            _context.Attach(user);
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
