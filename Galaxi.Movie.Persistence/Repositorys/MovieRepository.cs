using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Galaxi.Movie.Persistence.Repositorys
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContextDb _context;
        private readonly IDistributedCache _cache;
        private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(10);

        public MovieRepository(MovieContextDb context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task Add(Film movie)
        {
            _context.Add(movie);
            await _cache.RemoveAsync("movies_all");
        }

        public async Task Delete(Film movie)
        {
            _context.Movie.Remove(movie);
            await _cache.RemoveAsync($"movie_{movie.FilmId}");
            await _cache.RemoveAsync("movies_all");
        }

        public async Task Update(Film movie)
        {
            _context.Update(movie);
            await _cache.RemoveAsync($"movie_{movie.FilmId}");
            await _cache.RemoveAsync("movies_all");
        }

        public async Task<Film> GetMovieByIdAsync(Guid id)
        {
            var cacheKey = $"movie_{id}";
            var cacheMovie = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cacheMovie))
            {
                return JsonConvert.DeserializeObject<Film>(cacheMovie);
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(u => u.FilmId == id);

            if (movie != null)
            {
                await _cache.SetStringAsync
                              (cacheKey,JsonConvert.SerializeObject(movie),
                                new DistributedCacheEntryOptions
                                { 
                                  AbsoluteExpirationRelativeToNow = _cacheExpiration
                                }
                              );
            }
            return movie;
        }

        public async Task<IEnumerable<Film>> GetAllMoviesAsync()
        {
            var cacheKey = $"movies_all";
            var cacheMovie = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheMovie))
            {
                return JsonConvert.DeserializeObject<IEnumerable<Film>>(cacheMovie);
            }

            var movies = await _context.Movie.ToListAsync();

            if (movies != null && movies.Any())
            {
                await _cache.SetStringAsync
                              (cacheKey, JsonConvert.SerializeObject(movies),
                                new DistributedCacheEntryOptions
                                {
                                    AbsoluteExpirationRelativeToNow = _cacheExpiration
                                }
                              );
            }

            return movies;
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
