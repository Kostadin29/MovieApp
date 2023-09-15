namespace MovieApp.DTOs.DTOs.UserDTOs
{
    public class UserDto
    {      
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public int Age { get; set; }

    }
}
