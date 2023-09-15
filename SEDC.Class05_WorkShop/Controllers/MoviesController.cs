
namespace SEDC.Class05_WorkShop.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MovieApp.DataAccess.DataContext;
    using MovieApp.DTOs.DTOs.MovieDTOs;
    using MovieApp.Services.Interfaces;
    using SEDC.Class05_WorkShop.Models;
    using System.Linq.Expressions;

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieDto>>> GetAllMoviesAsync()
        {
            try
            {
                var movieDto = await _movieService.GetAllMovieAsync();
                if (movieDto == null)
                {
                    return NotFound();
                }


                return Ok(movieDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieAsync(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Id can not be a null.");
                }

                if (id <= 0)
                {
                    return BadRequest("Invalid input for Id. Please try again.");
                }



                MovieDto movieDto = await _movieService.GetMovieAsync(id);

                if (movieDto == null)
                {
                    return NotFound($"Movie with id: {id} not found.");
                }

                return Ok(movieDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");

            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovieAsync([FromBody] AddMovieDto addNoteDto)
        {
            try
            {

                if (addNoteDto == null || addNoteDto.Year == 0 || addNoteDto.Title == null || addNoteDto.Description == null || addNoteDto.Genre == 0)
                {
                    return BadRequest("Invalid input");
                }


                await _movieService.CreateMovieAsync(addNoteDto);

                return StatusCode(StatusCodes.Status201Created, "Movie added");
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");

            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id has invalid value");
                }

                MovieDto movieDto = await _movieService.GetMovieAsync(id);
                if (movieDto == null)
                {
                    //404
                    return NotFound($"Movie with id {id} was not found!");
                }

                await _movieService.DeleteMovieByIdAsync(movieDto.Id);
                return Ok(movieDto);

            }
            catch
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured, contact the admin!");
            }
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> EditNoteAsync([FromBody] UpdateMovieDto updateUserDto, int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid input");
                }

                await _movieService.EditMovieAsync(updateUserDto, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Please contact the support team.");

            }
        }

    }
}
