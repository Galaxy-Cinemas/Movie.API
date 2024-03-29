﻿using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Domain.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Galaxi.Movie.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("v1/[controller]/[action]")]
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
            var Test = HttpContext.Request.Headers["Authorizarion"];
            var movies = await _mediator.Send(new GetAllMoviesQuery());    
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                GetMovieByIdQuery movieById = new GetMovieByIdQuery(movieId: id);

                _log.LogInformation("Get movie {0}", id);
                var movie = await _mediator.Send(movieById);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatedMovieCommand movieToCreate)
        {
            var created = await _mediator.Send(movieToCreate);
            if (created)
                return Ok(movieToCreate);

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMovieCommand updateMovie)
        {
            if (id != updateMovie.MovieId)
            {
                return BadRequest();
            }

            var Update = await _mediator.Send(updateMovie);

            if (Update)
                return Ok(updateMovie);

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteMovieCommand movieId = new DeleteMovieCommand(movieId: id);
            
            var delete = await _mediator.Send(movieId);

            if (delete)
                return Ok();

            return BadRequest();
        }
    }
}
