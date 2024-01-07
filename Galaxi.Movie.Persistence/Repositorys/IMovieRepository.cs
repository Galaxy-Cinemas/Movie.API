using Galaxi.Movie.Data.Models;

namespace Galaxi.Movie.Persistence.Repositorys
{
    public interface IMovieRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<IEnumerable<Film>> GetMovieAsync();
        Task<Film> GetMovieById(int id);
        Task<bool> SaveAll();
        void Update<T>(T entity) where T : class;
    }
}