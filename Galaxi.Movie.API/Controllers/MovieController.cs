using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Domain.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxi.Movie.API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MovieController> _log;

        public MovieController(ILogger<MovieController> log, IMediator mediator) 
        {
            _mediator = mediator;
            _log = log;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var movies = await _mediator.Send(new GetAllMoviesQuery());

                if (movies == null || !movies.Any())
                {
                    return NotFound("No movies found.");
                }

                return Ok(movies);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("Billboard")]
        public async Task<IActionResult> GetAllMoviesBillboard()
        {
            var movies = await _mediator.Send(new GetAllMoviesQuery());
            return Ok(movies);
        }

        [HttpGet("{filmId}")]
        public async Task<IActionResult> GetById(Guid filmId)
        {
            try
            {
                var movie = await _mediator.Send(new GetMovieByIdQuery(filmId));
                if (movie == null)
                {
                    return NotFound("No movies found.");
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message, filmId);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedMovieCommand movieToCreate)
        {
            try
            {
                await _mediator.Send(movieToCreate);
                return Ok(movieToCreate);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
                
            }           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieCommand updateMovie)
        {
            if (id != updateMovie.FilmId)
            {
                return BadRequest("The movie ID does not match the film ID.");
            }
            try
            {
                var Update = await _mediator.Send(updateMovie);
                return Ok(updateMovie);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteMovieCommand FilmId = new DeleteMovieCommand(FilmId: id);
            try
            {
                var delete = await _mediator.Send(FilmId);
                return Ok();
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
