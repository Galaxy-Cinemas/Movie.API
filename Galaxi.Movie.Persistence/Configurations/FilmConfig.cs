using Galaxi.Movie.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galaxi.Movie.Persistence.Configurations
{
    public class FilmConfig : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder
                .ToTable("Movie", "DBO")
                .HasKey(x => new { x.MovieId });
        }
    }
}
