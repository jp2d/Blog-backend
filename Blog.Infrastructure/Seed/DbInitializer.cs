using Blog.Domain.Entity;
using Blog.Domain.Enum;
using Blog.Infrastructure.Context;

namespace Blog.Infrastructure.Seed
{
    public static class DbInitializer
    {
        public static void Initialize(BlogDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Name = "Admin",
                    Email = "admin@blog.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = Roles.Admin
                };

                context.Users.Add(admin);
                context.SaveChanges();
            }
        }
    }

}
