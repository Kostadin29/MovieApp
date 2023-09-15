
namespace MovieApp.DTOs.DTOs.MovieDTOs
{
    using SEDC.Class05_WorkShop.Enums;
    using System.ComponentModel.DataAnnotations;
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Year { get; set; }
        public GenreEnum Genre { get; set; }
    }
}
