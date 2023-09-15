
namespace MovieApp.DTOs.DTOs.MovieDTOs
{
    using SEDC.Class05_WorkShop.Enums;
    using System.ComponentModel.DataAnnotations;
    public class AddMovieDto
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(250, ErrorMessage = "Description cannot exceed 250 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Genre is required.")]
        public GenreEnum Genre { get; set; }
    }
}
