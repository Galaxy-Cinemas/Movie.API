using Galaxi.Movie.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Galaxi.Movie.Persistence.Configurations
{
    public class FilmConfig : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.ToTable("Films");

            builder.HasKey(f => f.FilmId);

            builder.Property(f => f.FilmId)
                .ValueGeneratedOnAdd();

            builder.Property(f => f.Title)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(f => f.Description)
                .HasMaxLength(500);

            builder.Property(f => f.PosterImage)
                .HasMaxLength(255);

            builder.Property(f => f.Director)
                .HasMaxLength(100);

            builder.Property(f => f.Genre)
                .HasMaxLength(100);

            builder.Property(f => f.Cast)
                .HasMaxLength(500);

            builder.Property(f => f.Origincountry)
                .HasMaxLength(100);

            builder.Property(f => f.Language)
                .HasConversion(
                    l => l.ToString(),
                    l => (Language)Enum.Parse(typeof(Language), l))
                .IsRequired();

            builder.Property(f => f.classification)
                .HasConversion(
                    c => c.ToString(),
                    c => (classification)Enum.Parse(typeof(classification), c))
                .IsRequired();

            #region HasData

            builder.HasData(
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Inception",
                   Description = "A thief who steals corporate secrets through the use of dream-sharing technology.",
                   PosterImage = "inception.jpg",
                   Director = "Christopher Nolan",
                   Genre = "Science Fiction",
                   Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
                   Origincountry = "USA",
                   Language = Language.English,
                   classification = classification.B,
                   duration = 148
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Parasite",
                   Description = "A poor family schemes to become employed by a wealthy family.",
                   PosterImage = "parasite.jpg",
                   Director = "Bong Joon-ho",
                   Genre = "Thriller",
                   Cast = "Song Kang-ho, Lee Sun-kyun, Cho Yeo-jeong",
                   Origincountry = "South Korea",
                   Language = Language.English,
                   classification = classification.B15,
                   duration = 132
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "The Godfather",
                   Description = "The aging patriarch of an organized crime dynasty transfers control to his reluctant son.",
                   PosterImage = "godfather.jpg",
                   Director = "Francis Ford Coppola",
                   Genre = "Crime",
                   Cast = "Marlon Brando, Al Pacino, James Caan",
                   Origincountry = "USA",
                   Language = Language.English,
                   classification = classification.B15,
                   duration = 175
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Amélie",
                   Description = "Amélie is an innocent and naive girl in Paris with her own sense of justice.",
                   PosterImage = "amelie.jpg",
                   Director = "Jean-Pierre Jeunet",
                   Genre = "Romantic Comedy",
                   Cast = "Audrey Tautou, Mathieu Kassovitz, Rufus",
                   Origincountry = "France",
                   Language = Language.French,
                   classification = classification.AA,
                   duration = 122
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Pulp Fiction",
                   Description = "The lives of two mob hitmen, a boxer, and a pair of bandits intertwine in four tales of violence.",
                   PosterImage = "pulpfiction.jpg",
                   Director = "Quentin Tarantino",
                   Genre = "Crime",
                   Cast = "John Travolta, Uma Thurman, Samuel L. Jackson",
                   Origincountry = "USA",
                   Language = Language.English,
                   classification = classification.C,
                   duration = 154
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Coco",
                   Description = "Aspiring musician Miguel, confronted with his family's ancestral ban on music, enters the Land of the Dead.",
                   PosterImage = "coco.jpg",
                   Director = "Lee Unkrich",
                   Genre = "Animation",
                   Cast = "Anthony Gonzalez, Gael García Bernal, Benjamin Bratt",
                   Origincountry = "USA",
                   Language = Language.English,
                   classification = classification.AA,
                   duration = 105
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "The Dark Knight",
                   Description = "Batman begins his fight against crime in Gotham.",
                   PosterImage = "darkknight.jpg",
                   Director = "Christopher Nolan",
                   Genre = "Action",
                   Cast = "Christian Bale, Heath Ledger, Aaron Eckhart",
                   Origincountry = "USA",
                   Language = Language.English,
                   classification = classification.B,
                   duration = 152
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Spirited Away",
                   Description = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits.",
                   PosterImage = "spiritedaway.jpg",
                   Director = "Hayao Miyazaki",
                   Genre = "Animation",
                   Cast = "Rumi Hiiragi, Miyu Irino, Mari Natsuki",
                   Origincountry = "Japan",
                   Language = Language.English,
                   classification = classification.A,
                   duration = 125
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Pan's Labyrinth",
                   Description = "In the falangist Spain of 1944, the bookish young stepdaughter of a sadistic army officer escapes into an eerie but captivating fantasy world.",
                   PosterImage = "panslabyrinth.jpg",
                   Director = "Guillermo del Toro",
                   Genre = "Fantasy",
                   Cast = "Ivana Baquero, Ariadna Gil, Sergi López",
                   Origincountry = "Spain",
                   Language = Language.Spanish,
                   classification = classification.B15,
                   duration = 118
               },
               new Film
               {
                   FilmId = Guid.NewGuid(),
                   Title = "Schindler's List",
                   Description = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce.",
                   PosterImage = "schindlerslist.jpg",
                   Director = "Steven Spielberg",
                   Genre = "Biography",
                   Cast = "Liam Neeson, Ralph Fiennes, Ben Kingsley",
                   Origincountry = "USA",
                   Language = Language.English,
                   classification = classification.C,
                   duration = 195
               }
           );

            #endregion

        }
    }
}
