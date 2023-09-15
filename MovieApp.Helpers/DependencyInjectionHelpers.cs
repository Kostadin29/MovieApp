
namespace MovieApp.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using MovieApp.DataAccess.DataContext;
    using MovieApp.DataAccess.Implementations;
    using MovieApp.DataAccess.Interfaces;
    using MovieApp.Services.Implementations;
    using MovieApp.Services.Interfaces;
    public static class DependencyInjectionHelpers
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MovieAppDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMovieService, MovieService>();
        }
    }
}
