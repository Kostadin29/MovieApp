
namespace MovieApp.DataAccess.Interfaces
{
    using MovieApp.Domain.Models;
    public interface IRepository<T> where T : BaseEntity
    {
        // CRUD Operations
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
