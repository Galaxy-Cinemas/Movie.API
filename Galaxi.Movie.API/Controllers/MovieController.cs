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
        private readonly MovieContextDb _dbcontext;

        public MovieController(IMediator mediator, MovieContextDb dbcontext) 
        {
            _mediator = mediator;
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _mediator.Send(new GetAllMoviesQuery());
            //var moviestest = await _dbcontext.Movie.ToListAsync(); //TODO
                
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

            var functionToUpdate = await _mediator.Send(updateMovie);

            if (functionToUpdate)
                return Ok(updateMovie);

            return BadRequest();
        }
    }
}
