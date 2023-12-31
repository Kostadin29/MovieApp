﻿namespace MovieApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Age { get; set; }
        public List<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}
