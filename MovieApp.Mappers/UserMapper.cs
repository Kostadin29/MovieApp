

namespace MovieApp.Mappers
{
    using MovieApp.Domain.Models;
    using MovieApp.DTOs.DTOs.UserDTOs;
    public static class UserMapper
    {
        public static UserDto MapToUserDto(this User user)
        {
            return new UserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Age = user.Age
            };
        }
    }
}
