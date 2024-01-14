using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Galaxi.Movie.Persistence.Repositorys
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContextDb _context;

        public MovieRepository(MovieContextDb context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<Film> GetMovieById(int id)
        {
            var movie = await _context.Movie.FirstOrDefaultAsync(u => u.MovieId == id);
            return movie;
        }

        public async Task<IEnumerable<Film>> GetMovieAsync()
        {
            var movie = await _context.Movie.ToListAsync();
            return movie;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
