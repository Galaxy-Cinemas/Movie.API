using Microsoft.AspNetCore.Mvc;

namespace Galaxi.Movie.API.Controllers
{
    public class MovieController : ControllerBase
    {
        public MovieController() { }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           return NoContent();
        }
    }
}
