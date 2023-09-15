
namespace MovieApp.Services.Interfaces
{
    using MovieApp.DTOs.DTOs.MovieDTOs;
    using SEDC.Class05_WorkShop.Enums;
    public interface IMovieService
    {
        List<MovieDto> FilterMovies(int? year, GenreEnum? genre);
        Task<MovieDto> GetMovieAsync(int id);
        Task<List<MovieDto>> GetAllMovieAsync();
        Task CreateMovieAsync(AddMovieDto createMovie);
        Task DeleteMovieByIdAsync(int id);

        Task EditMovieAsync(UpdateMovieDto updateMovieDto, int id);

    }
}
