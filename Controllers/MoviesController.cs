using Microsoft.AspNetCore.Mvc;
using FilmLogAPI.Services;

namespace FilmLogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string t)
        {
            if (string.IsNullOrEmpty(t))
                return BadRequest("Movie title is required.");

            var result = await _movieService.SearchMovie(t);
            return Ok(result);
        }
    }
}