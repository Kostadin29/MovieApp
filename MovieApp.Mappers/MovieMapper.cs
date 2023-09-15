
namespace MovieApp.Mappers
{
    using MovieApp.DTOs.DTOs.MovieDTOs;
    using SEDC.Class05_WorkShop.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class MovieMapper
    {
        public static Movie ToMovie(this AddMovieDto addMovieDto)
        {
            return new Movie
            {
                Year = addMovieDto.Year,
                Description = addMovieDto.Description,
                Genre = addMovieDto.Genre,
                Title = addMovieDto.Title
            };
        }

        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto
            {
                Id = movie.Id,
                Description = movie.Description,
                Genre = movie.Genre,
                Title = movie.Title,
                Year = movie.Year
            };
        }
    }
}
