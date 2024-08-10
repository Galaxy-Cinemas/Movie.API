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
                var successResponse = ResponseHandler<IEnumerable<FilmSummaryDto>>.CreateSuccessResponse("Movie retrieved successfully", movies);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (KeyNotFoundException ex)
            {
                var response = ResponseHandler<string>.CreateNotFoundResponse("Movies not found.", ex.Message);
                return StatusCode(response.StatusCode.Value, response);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
        }

        [HttpGet("{filmId}")]
        public async Task<IActionResult> GetById(Guid filmId)
        {
            try
            {
                var movie = await _mediator.Send(new GetMovieByIdQuery(filmId));

                var successResponse = ResponseHandler<FilmDetailsDto>.CreateSuccessResponse("Movie created successfully", movie);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
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

                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Validation failed", errors);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            try
            {
                await _mediator.Send(movieToCreate);
                var successResponse = ResponseHandler<CreatedMovieCommand>.CreateSuccessResponse("Movie created successfully", movieToCreate);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (InvalidOperationException ex)
            {
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMovieCommand updateMovie)
        {
            if (id != updateMovie.FilmId)
            {
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred",new List<string> {"The movie ID does not match the film ID." });
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            try
            {
                var Update = await _mediator.Send(updateMovie);

                var successResponse = ResponseHandler<UpdateMovieCommand>.CreateSuccessResponse("Movie updated successfully", updateMovie);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (KeyNotFoundException ex)
            {
                var response = ResponseHandler<string>.CreateNotFoundResponse("Movie not found.", "The movie with the specified ID does not exist.");
                return StatusCode(response.StatusCode.Value, response);
            }
            catch (InvalidOperationException ex)
            {
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeleteMovieCommand FilmId = new DeleteMovieCommand(FilmId: id);
            try
            {
                var delete = await _mediator.Send(FilmId);
                var successResponse = ResponseHandler<string>.CreateSuccessResponse("Movie deleted successfully", null);
                return StatusCode(successResponse.StatusCode.Value, successResponse);
            }
            catch (KeyNotFoundException ex)
            {
                var response = ResponseHandler<string>.CreateNotFoundResponse("Movie not found.", "The movie with the specified ID does not exist.");
                return StatusCode(response.StatusCode.Value, response);
            }
            catch (InvalidOperationException ex)
            {
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("Failed to save changes to the database.", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
            catch (Exception ex)
            {
                _log.LogInformation(ex.Message);
                var errorResponse = ResponseHandler<string>.CreateErrorResponse("An internal server error occurred", ex);
                return StatusCode(errorResponse.StatusCode.Value, errorResponse);
            }
        }
    }
}
