using Galaxi.Movie.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Galaxi.Movie.Persistence.Persistence
{
    public class MovieContextDb : DbContext, IMovieContextDb
    {
        private readonly IConfiguration? _configuration;

        public MovieContextDb()
        { }

        public MovieContextDb(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            base.ChangeTracker.LazyLoadingEnabled = false;
            _configuration = configuration;
        }
        public DbSet<Film> Movie { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
