
namespace MovieApp.Domain.Models
{
    using SEDC.Class05_WorkShop.Models;

    public class UserMovie : BaseEntity
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }

    }
}
