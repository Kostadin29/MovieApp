
namespace MovieApp.Services.Interfaces
{
    using MovieApp.DTOs.DTOs.UserDTOs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task RegisterUser(RegisterUserDto registerUserDto);

        //This returns string because it will return the JWT Token, that token will be passed with every request
        Task<string> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
