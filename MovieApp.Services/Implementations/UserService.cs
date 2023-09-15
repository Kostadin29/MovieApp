
namespace MovieApp.Services.Implementations
{
    using Microsoft.IdentityModel.Tokens;
    using MovieApp.DataAccess.Interfaces;
    using MovieApp.Domain.Models;
    using MovieApp.DTOs.DTOs.UserDTOs;
    using MovieApp.Services.Interfaces;
    using MovieApp.Shared;
    using MovieApp.Mappers;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using XSystem.Security.Cryptography;
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            List<User> users = await _userRepository.GetAllAsync();

            if (users == null)
            {
                throw new Exception("Users are null");
            }

            return users.Select(x => x.MapToUserDto()).ToList();
        }

        public async Task<string> LoginUserAsync(LoginUserDto loginUserDto)
        {
            if (string.IsNullOrEmpty(loginUserDto.Username) || string.IsNullOrEmpty(loginUserDto.Password))
            {
                throw new UserException("Username and password are required");
            }

            //hash password
            //MD5 hash algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(loginUserDto.Password);

            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            string hash = Encoding.ASCII.GetString(hashBytes);

            User userDb = await _userRepository.LoginUser(loginUserDto.Username, hash);

            if (userDb == null)
            {
                throw new UserException("User not found");
            }

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            byte[] secretKeyBytes = Encoding.ASCII.GetBytes("Our very secret code");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName",$"{userDb.FirstName} {userDb.LastName}")
                    })
            };

            //generate token
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            //convert to string and return it
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public async Task RegisterUser(RegisterUserDto registerUserDto)
        {
            //1. Validate the user with our private method
            await ValidateUser(registerUserDto);

            //2. Hash the password
            //MD5 hash algorithm
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();

            //Test123 -> 94378439
            byte[] passwordBytes = Encoding.ASCII.GetBytes(registerUserDto.Password);

            //get the bytes of the hash string/password
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);

            //get the hash as string
            string hash = Encoding.ASCII.GetString(hashBytes);

            //3. Create the User and add it to the DB
            User user = new User()
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Age = registerUserDto.Age,
                Password = hash
            };

            await _userRepository.CreateAsync(user);
        }

        //This is a private method because it is only used in this class
        private async Task ValidateUser(RegisterUserDto user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.ConfirmedPassword))
            {
                throw new UserException("Username and password are required.");
            }

            if (user.Username.Length > 30)
            {
                throw new UserException("Username max length is 30 characters.");
            }

            if (!string.IsNullOrEmpty(user.FirstName) && user.FirstName.Length > 50)
            {
                throw new UserException("Max length for FirstName field is 50 characters.");
            }

            if (!string.IsNullOrEmpty(user.LastName) && user.LastName.Length > 50)
            {
                throw new UserException("Max length for LastName field is 50 characters.");
            }

            if (user.Password != user.ConfirmedPassword)
            {
                throw new UserException("Passwords do not match.");
            }

            var userDb = await _userRepository.GetUserByUsername(user.Username);

            if (userDb != null)
            {
                throw new UserException($"The username {user.Username} is already in use.");
            }
        }
    }
}
