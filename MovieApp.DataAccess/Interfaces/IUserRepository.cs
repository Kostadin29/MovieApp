
namespace MovieApp.DataAccess.Interfaces
{
    using MovieApp.Domain.Models;
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsername(string username);
        Task<User> LoginUser(string username, string hashedPassword);
    }
}
