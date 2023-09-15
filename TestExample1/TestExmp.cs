using Microsoft.EntityFrameworkCore;
using MovieApp.DataAccess.DataContext;
using MovieApp.DataAccess.Implementations;
using MovieApp.DataAccess.Interfaces;
using MovieApp.Services.Implementations;
using MovieApp.Services.Interfaces;

namespace TestExample1
{
    [TestClass]

    public class TestExmp
    {

        private IMovieRepository _movieRepository;
        private IMovieService _movieService;
        private MovieAppDbContext _dbContext;

        [TestInitialize]
        public void MoviesTestInitialize()
        {
            var builder = new DbContextOptionsBuilder<MovieAppDbContext>()
                .UseSqlServer("Data Source=(localdb)\\MovieAppWorkShop;Database=MovieAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

            _dbContext = new MovieAppDbContext(builder.Options);

            //The database will reset all the time the tests are ran
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.Migrate();

            _movieRepository = new MovieRepository(_dbContext);
            _movieService = new MovieService(_movieRepository);
        }
    }
}
