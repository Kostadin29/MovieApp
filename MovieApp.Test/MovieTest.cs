
namespace MovieApp.Test
{
    using Microsoft.EntityFrameworkCore;
    using MovieApp.DataAccess.DataContext;
    using MovieApp.DataAccess.Implementations;
    using MovieApp.DataAccess.Interfaces;
    using MovieApp.Services.Implementations;
    using MovieApp.Services.Interfaces;
    using SEDC.Class05_WorkShop.Models;

    [TestClass]
    public class MovieTest
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

        [TestMethod]
        public async Task GetByIdAsync_GetMovieById_True()
        {
            // Arrange 
            var movie = new Movie()
            {
                Title = "Pulp Fiction",
                Description = "A non-linear crime masterpiece from 1994, interweaving quirky characters, dark humor, and unexpected twists.",
                Year = 1994,
                Genre = (SEDC.Class05_WorkShop.Enums.GenreEnum)3
            };

            _dbContext.Add(movie);
            _dbContext.SaveChangesAsync();

            // Act 
            var result = await _movieRepository.GetByIdAsync(1);

            // Assert 
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetAllAsync_True()
        {
            // Arrange 
            var movie1 = new Movie()
            {
                Title = "Pulp Fiction",
                Description = "A non-linear crime masterpiece from 1994, interweaving quirky characters, dark humor, and unexpected twists.",
                Year = 1994,
                Genre = (SEDC.Class05_WorkShop.Enums.GenreEnum)3
            };

            var movie2 = new Movie()
            {
                Title = "Jurassic Park",
                Description = "An adventure turns chaotic when genetically resurrected dinosaurs run amok in a theme park.",
                Year = 1993,
                Genre = (SEDC.Class05_WorkShop.Enums.GenreEnum)8
            };

            _dbContext.Add(movie1);
            _dbContext.Add(movie2);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _movieRepository.GetAllAsync();

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetMovieAsync_FromService_True()
        {
            // Arrange
            var movie1 = new Movie()
            {
                Title = "Pulp Fiction",
                Description = "A non-linear crime masterpiece from 1994, interweaving quirky characters, dark humor, and unexpected twists.",
                Year = 1994,
                Genre = (SEDC.Class05_WorkShop.Enums.GenreEnum)3
            };

            var movie2 = new Movie()
            {
                Title = "Jurassic Park",
                Description = "An adventure turns chaotic when genetically resurrected dinosaurs run amok in a theme park.",
                Year = 1993,
                Genre = (SEDC.Class05_WorkShop.Enums.GenreEnum)8
            };

            _dbContext.Add(movie1);
            _dbContext.Add(movie2);
            await _dbContext.SaveChangesAsync();

            // Act 
            var result = await _movieService.GetAllMovieAsync();

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetMovieAsync_WithIdFromService_Exception()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "Pulp Fiction",
                Description = "A non-linear crime masterpiece from 1994, interweaving quirky characters, dark humor, and unexpected twists.",
                Year = 1994,
                Genre = (SEDC.Class05_WorkShop.Enums.GenreEnum)3
            };

            _dbContext.Add(movie);
            _dbContext.SaveChanges();

            // Assert 
            await Assert.ThrowsExceptionAsync<Exception>(async () => await _movieService.GetMovieAsync(1000));
        }

        [TestMethod]
        public async Task CreateMovieAsync_FromService_ExistingMovie_ReturnsNotNull()
        {
            // Arrange
            var movie = new Movie()
            {
                Title = "Pulp Fiction",
                Description = "A non-linear crime masterpiece from 1994, interweaving quirky characters, dark humor, and unexpected twists.",
                Year = 1994,
                Genre = (SEDC.Class05_WorkShop.Enums.GenreEnum)3
            };

            _dbContext.Add(movie);
            await _dbContext.SaveChangesAsync();

            // Act 
            var result = await _movieService.GetMovieAsync(1);

            // Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

    }
}
