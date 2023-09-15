
namespace MovieApp.Services.Implementations
{
    using MovieApp.DataAccess.Interfaces;
    using MovieApp.DTOs.DTOs.MovieDTOs;
    using MovieApp.Services.Interfaces;
    using MovieApp.Shared;
    using MovieApp.Mappers;
    using SEDC.Class05_WorkShop.Enums;
    using SEDC.Class05_WorkShop.Models;
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository _movieRepository)
        {
            this._movieRepository = _movieRepository;
        }

        public async Task CreateMovieAsync(AddMovieDto createMovie)
        {
            Movie movieEntity = createMovie.ToMovie();

            await _movieRepository.CreateAsync(movieEntity);
        }

        public async Task DeleteMovieByIdAsync(int id)
        {
            await _movieRepository.DeleteAsync(id);
        }

        public async Task EditMovieAsync(UpdateMovieDto updateMovieDto, int id)
        {
            Movie movieDb = await _movieRepository.GetByIdAsync(id);

            if (movieDb == null)
            {
                throw new Exception("Movie not found");
            }

            movieDb.Year = updateMovieDto.Year;
            movieDb.Description = updateMovieDto.Description;
            movieDb.Title = updateMovieDto.Title;
            movieDb.Genre = updateMovieDto.Genre;


            await _movieRepository.UpdateAsync(movieDb);
        }

        public List<MovieDto> FilterMovies(int? year, GenreEnum? genre)
        {
            if (genre.HasValue)
            {
                //validate if the value for genre is valid
                var enumValues = Enum.GetValues(typeof(GenreEnum))
                                        .Cast<GenreEnum>()
                                        .ToList();

                if (!enumValues.Contains(genre.Value))
                {
                    throw new MovieException("Invalid genre value");
                }
            }
            return _movieRepository.FilterMovies(year, genre)
                .Select(x => x.ToMovieDto()).ToList();
        }

        public async Task<List<MovieDto>> GetAllMovieAsync()
        {
            List<Movie> movies = await _movieRepository.GetAllAsync();

            if (movies == null)
            {
                throw new Exception("Movies are null");
            }

            return movies.Select(movie => movie.ToMovieDto()).ToList();
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            Movie movieDb = await _movieRepository.GetByIdAsync(id);

            if (movieDb == null)
            {
                throw new Exception("Movie is null");
            }

            return movieDb.ToMovieDto();
        }
    }
}
