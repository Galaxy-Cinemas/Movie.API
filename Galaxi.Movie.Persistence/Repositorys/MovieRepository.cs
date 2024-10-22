using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Galaxi.Movie.Persistence.Repositorys
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContextDb _context;
        private readonly ILogger<MovieRepository> _log;

        public MovieRepository(MovieContextDb context, ILogger<MovieRepository> log)
        {
            _context = context;
            _log = log;
        }

        public async Task<IEnumerable<Film>> GetAllMoviesAsync()
        {
            var movies = await _context.Movie.ToListAsync();
            return movies;
        }

        public async Task<Film> GetMovieByIdAsync(Guid id)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(u => u.FilmId == id);
            return movie;
        }

        public async Task Add(Film movie)
        {
            _context.Add(movie);
        }

        public async Task Delete(Film movie)
        {
            _context.Movie.Remove(movie);
        }

        public async Task Update(Film movie)
        {
            _context.Update(movie);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task MigrateAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
