
namespace MovieApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MovieApp.DTOs.DTOs.UserDTOs;
    using MovieApp.Services.Interfaces;
    using MovieApp.Shared;

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                return Ok(await _userService.GetAllUsersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");

            }
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto user)
        {
            try
            {
                await _userService.RegisterUser(user);
                return StatusCode(StatusCodes.Status201Created, "User created");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto user)
        {
            try
            {
                string token = await _userService.LoginUserAsync(user);
                return Ok(token);
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }


    }
}
