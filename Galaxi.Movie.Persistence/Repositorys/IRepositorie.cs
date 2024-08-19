using Galaxi.Movie.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Movie.Persistence.Repositorys
{
    public interface IRepository
    {
        Task Add(Film movie);
        Task Delete(Film movie);
        Task Update(Film movie);
        Task<bool> SaveAll();
    }
}
