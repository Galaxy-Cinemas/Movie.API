using Galaxi.Movie.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;

namespace Galaxi.Movie.Persistence.Persistence
{
    public interface IMovieContextDb
    {
        DbSet<Film> Movie { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}