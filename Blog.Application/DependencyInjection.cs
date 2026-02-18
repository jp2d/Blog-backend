using Blog.Application.Interfaces;
using Blog.Application.Services;
using Blog.Domain.Interfaces;
using Blog.Infrastructure.Context;
using Blog.Infrastructure.Repository;
using Blog.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            #region EF Core
            services.AddDbContext<BlogDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default"))
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));
            #endregion

            #region Repositories
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Authentication
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthService>();
            #endregion

            #region Serviços de aplicação
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            #endregion

            return services;
        }

        public static void InitializeDatabase(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BlogDbContext>();
            DbInitializer.Initialize(context);
        }
    }
}
