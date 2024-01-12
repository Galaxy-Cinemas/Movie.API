﻿
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Domain.Infrastructure.Queries;
using Galaxi.Movie.Persistence.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Galaxi.Movie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _mediator.Send(new GetAllMoviesQuery());    
            return Ok(movies);
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
        public async Task<IActionResult> Delete(DeleteMovieCommand id)
        {
            var delete = await _mediator.Send(id);

            if (delete)
                return Ok("removed function");

            return BadRequest();

        }
    }
}
