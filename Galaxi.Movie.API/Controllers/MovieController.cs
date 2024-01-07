
using Galaxi.Movie.Domain.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
