
namespace MovieApp.DataAccess.Implementations
{
    using Microsoft.EntityFrameworkCore;
    using MovieApp.DataAccess.DataContext;
    using MovieApp.DataAccess.Interfaces;
    using SEDC.Class05_WorkShop.Enums;
    using SEDC.Class05_WorkShop.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MovieRepository : IMovieRepository
    {
        private readonly MovieAppDbContext _movieAppDbContext;

        public MovieRepository(MovieAppDbContext _movieAppDbContext)
        {
            this._movieAppDbContext = _movieAppDbContext;
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            return await _movieAppDbContext.Movies.ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _movieAppDbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task CreateAsync(Movie entity)
        {
            await _movieAppDbContext.Movies.AddAsync(entity);
            await _movieAppDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

            Movie movieDb = await GetByIdAsync(id);

            _movieAppDbContext.Remove(movieDb);
            await _movieAppDbContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Movie entity)
        {
            _movieAppDbContext.Movies.Update(entity);
            await _movieAppDbContext.SaveChangesAsync();
        }

        public List<Movie> FilterMovies(int? year, GenreEnum? genre)
        {
            if (genre == null && year == null)
            {
                return _movieAppDbContext.Movies.ToList();
            }    

            if(year == null)
            {
                List<Movie> moviesDb = _movieAppDbContext.Movies.Where(x => x.Genre == genre).ToList();
                return moviesDb;
            }

            if(genre == null)
            {
                List<Movie> moviesDb = _movieAppDbContext.Movies.Where(x => x.Year == year).ToList();
                return moviesDb;
            }

            List<Movie> movies = _movieAppDbContext.Movies.Where(x => x.Year == year && x.Genre == genre).ToList();

            return movies;

        }
    }
}
