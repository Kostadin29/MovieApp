
namespace SEDC.Class05_WorkShop.Models
{
    using MovieApp.Domain.Models;
    using SEDC.Class05_WorkShop.Enums;
    public class Movie : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public GenreEnum Genre { get; set; }
        public List<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}
