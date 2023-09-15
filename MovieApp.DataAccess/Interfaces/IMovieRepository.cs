
namespace MovieApp.DataAccess.Interfaces
{
    using SEDC.Class05_WorkShop.Enums;
    using SEDC.Class05_WorkShop.Models;
    public interface IMovieRepository : IRepository<Movie>
    {
        List<Movie> FilterMovies(int? year, GenreEnum? genre);
    }
}
