using Galaxi.Movie.Data.Models;

namespace Galaxi.Movie.Persistence.Repositorys
{
    public interface IMovieRepository : IRepositorie
    {
        Task<IEnumerable<Film>> GetMovieAsync();
        Task<Film> GetMovieById(Guid id);
    }
}