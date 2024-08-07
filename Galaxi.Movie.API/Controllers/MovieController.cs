using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.DTOs;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Domain.Infrastructure.Queries;
using Galaxi.Movie.Domain.Response;
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
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Resource not found",
                        StatusCode = StatusCodes.Status404NotFound
                    });
                }

                return Ok(
                   new ApiResponse<IEnumerable<FilmSummaryDto>>
                   {
                       Success = true,
                       Message = "Movie retrieved successfully",
                       Data = movies,
                       StatusCode = StatusCodes.Status200OK
                   });
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                return BadRequest(
                    new ApiResponse<FilmDetailsDto>
                    {
                        Success = true,
                        Message = "An internal server error occurred",
                        Data = null,
                        Errors = new List<string> { ex.Message },
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
            }
        }

        [HttpGet("{filmId}")]
        public async Task<IActionResult> GetById(Guid filmId)
        {
            try
            {
                var movie = await _mediator.Send(new GetMovieByIdQuery(filmId));
                if (movie == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Success = false,
                        Message = "Movie not found",
                        Errors = new List<string> { "The movie with the specified ID does not exist."}
                    });
                }
                return Ok(
                   new ApiResponse<FilmDetailsDto>
                   {
                       Success = true,
                       Message = "Movie retrieved successfully",
                       Data = movie,
                       StatusCode = StatusCodes.Status200OK
                   });
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message, filmId);
                return BadRequest(
                    new ApiResponse<FilmDetailsDto>
                    {
                        Success = true,
                        Message = "An internal server error occurred",
                        Data = null,
                        Errors = new List<string> { ex.Message },
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatedMovieCommand movieToCreate)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "Validation failed",
                    Errors = errors
                });
            }
            try
            {
                await _mediator.Send(movieToCreate);
                return Ok(
                    new ApiResponse<CreatedMovieCommand>
                    {
                        Success = true,
                        Message = "Movie created successfully",
                        Data = movieToCreate,
                        StatusCode = StatusCodes.Status200OK
                    });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(
                new ApiResponse<string>
                {
                    Success = false,
                    Message = "Failed to save changes to the database.",
                    Errors = new List<string> { ex.Message },
                    StatusCode = StatusCodes.Status404NotFound
                });

            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                return BadRequest(
                    new ApiResponse<FilmDetailsDto>
                    {
                        Success = true,
                        Message = "An internal server error occurred",
                        Data = null,
                        Errors = new List<string> { ex.Message },
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieCommand updateMovie)
        {
            if (id != updateMovie.FilmId)
            {
                return BadRequest(new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Success = false,
                    Message = "The movie ID does not match the film ID.",
                });
            }
            try
            {
                var Update = await _mediator.Send(updateMovie);

                return Ok(
                   new ApiResponse<UpdateMovieCommand>
                   {
                       Success = true,
                       Message = "Movie updated successfully",
                       Data = updateMovie,
                       StatusCode = StatusCodes.Status200OK
                   });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Movie not found.",
                        Errors = new List<string> { ex.Message },
                        StatusCode = StatusCodes.Status404NotFound
                    });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(
                new ApiResponse<string>
                {
                    Success = false,
                    Message = "Failed to save changes to the database.",
                    Errors = new List<string> { ex.Message },
                    StatusCode = StatusCodes.Status404NotFound
                });

            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                return BadRequest(
                    new ApiResponse<FilmDetailsDto>
                    {
                        Success = true,
                        Message = "An internal server error occurred",
                        Data = null,
                        Errors = new List<string> { ex.Message },
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteMovieCommand FilmId = new DeleteMovieCommand(FilmId: id);
            try
            {
                var delete = await _mediator.Send(FilmId);
                return Ok(
                    new ApiResponse<string>
                    {
                        Success = true,
                        Message = "Movie deleted successfully",
                        StatusCode = StatusCodes.Status200OK
                    });
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(
                        new ApiResponse<string>
                        {
                            Success = false,
                            Message = "Movie not found.",
                            Errors = new List<string> { ex.Message },
                            StatusCode = StatusCodes.Status404NotFound
                        });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(
                new ApiResponse<string>
                {
                    Success = false,
                    Message = "Failed to save changes to the database.",
                    Errors = new List<string> { ex.Message },
                    StatusCode = StatusCodes.Status500InternalServerError
                });

            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                return BadRequest(
                    new ApiResponse<FilmDetailsDto>
                    {
                        Success = true,
                        Message = "An internal server error occurred",
                        Data = null,
                        Errors = new List<string> { ex.Message },
                        StatusCode = StatusCodes.Status500InternalServerError
                    });
            }
        }
    }
}
