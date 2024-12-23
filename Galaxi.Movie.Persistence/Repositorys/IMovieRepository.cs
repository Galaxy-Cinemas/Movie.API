using Galaxi.Movie.Data.Models;

namespace Galaxi.Movie.Persistence.Repositorys
{
    public interface IMovieRepository 
    {
        Task Add(Film movie);
        Task Delete(Film movie);
        Task Update(Film movie);
        Task<IEnumerable<Film>> GetAllMoviesAsync();
        Task<Film> GetMovieByIdAsync(Guid id);
        Task<bool> SaveAll();
        Task MigrateAsync();
    }
}