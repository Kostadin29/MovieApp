
namespace MovieApp.DataAccess.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using MovieApp.DataAccess.DataContext;
    using MovieApp.DataAccess.Interfaces;
    using MovieApp.Domain.Models;
    public class UserRepository : IUserRepository
    {
        private readonly MovieAppDbContext _movieAppDbContext;

        public UserRepository(MovieAppDbContext _movieAppDbContext)
        {
            this._movieAppDbContext = _movieAppDbContext;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _movieAppDbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _movieAppDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(User entity)
        {
            await _movieAppDbContext.Users.AddAsync(entity);
            await _movieAppDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User userDb = await _movieAppDbContext.Users.SingleOrDefaultAsync(x => x.Id == id);

            if (userDb == null)
            {
                throw new Exception($"Item with id: {id} was not found!");
            }

            _movieAppDbContext.Users.Remove(userDb);
            await _movieAppDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _movieAppDbContext.Update(entity);
            await _movieAppDbContext.SaveChangesAsync();
        }



        public async Task<User> GetUserByUsername(string username)
        {
            return await _movieAppDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task<User> LoginUser(string username, string hashedPassword)
        {
            return await _movieAppDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == hashedPassword);

        }


    }
}
