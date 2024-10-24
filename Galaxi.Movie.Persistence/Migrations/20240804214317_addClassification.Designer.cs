﻿// <auto-generated />
using System;
using Galaxi.Movie.Persistence.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Galaxi.Movie.Persistence.Migrations
{
    [DbContext(typeof(MovieContextDb))]
    [Migration("20240804214317_addClassification")]
    partial class addClassification
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Galaxi.Movie.Data.Models.Film", b =>
                {
                    b.Property<Guid>("FilmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cast")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Director")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Genre")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Origincountry")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PosterImage")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("classification")
                        .HasColumnType("int");

                    b.Property<int?>("duration")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("FilmId");

                    b.ToTable("Films", (string)null);

                    b.HasData(
                        new
                        {
                            FilmId = new Guid("00f5294e-b165-4851-b5cf-7487f047d250"),
                            Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page",
                            Description = "A thief who steals corporate secrets through the use of dream-sharing technology.",
                            Director = "Christopher Nolan",
                            Genre = "Science Fiction",
                            Language = 0,
                            Origincountry = "USA",
                            PosterImage = "inception.jpg",
                            Title = "Inception",
                            classification = 2,
                            duration = 148
                        },
                        new
                        {
                            FilmId = new Guid("fc43f07a-01cb-43af-b6a8-be5851e37643"),
                            Cast = "Song Kang-ho, Lee Sun-kyun, Cho Yeo-jeong",
                            Description = "A poor family schemes to become employed by a wealthy family.",
                            Director = "Bong Joon-ho",
                            Genre = "Thriller",
                            Language = 0,
                            Origincountry = "South Korea",
                            PosterImage = "parasite.jpg",
                            Title = "Parasite",
                            classification = 3,
                            duration = 132
                        },
                        new
                        {
                            FilmId = new Guid("6a2250e0-5eef-4e36-9055-2e9aaae3e39b"),
                            Cast = "Marlon Brando, Al Pacino, James Caan",
                            Description = "The aging patriarch of an organized crime dynasty transfers control to his reluctant son.",
                            Director = "Francis Ford Coppola",
                            Genre = "Crime",
                            Language = 0,
                            Origincountry = "USA",
                            PosterImage = "godfather.jpg",
                            Title = "The Godfather",
                            classification = 3,
                            duration = 175
                        },
                        new
                        {
                            FilmId = new Guid("828c9ca0-687a-4ec9-a4b1-5730071cbe19"),
                            Cast = "Audrey Tautou, Mathieu Kassovitz, Rufus",
                            Description = "Amélie is an innocent and naive girl in Paris with her own sense of justice.",
                            Director = "Jean-Pierre Jeunet",
                            Genre = "Romantic Comedy",
                            Language = 2,
                            Origincountry = "France",
                            PosterImage = "amelie.jpg",
                            Title = "Amélie",
                            classification = 0,
                            duration = 122
                        },
                        new
                        {
                            FilmId = new Guid("a44240d1-a5a4-426f-8059-2b8bf154693d"),
                            Cast = "John Travolta, Uma Thurman, Samuel L. Jackson",
                            Description = "The lives of two mob hitmen, a boxer, and a pair of bandits intertwine in four tales of violence.",
                            Director = "Quentin Tarantino",
                            Genre = "Crime",
                            Language = 0,
                            Origincountry = "USA",
                            PosterImage = "pulpfiction.jpg",
                            Title = "Pulp Fiction",
                            classification = 4,
                            duration = 154
                        },
                        new
                        {
                            FilmId = new Guid("fc1484aa-c69e-482c-85a9-f158c71e5e25"),
                            Cast = "Anthony Gonzalez, Gael García Bernal, Benjamin Bratt",
                            Description = "Aspiring musician Miguel, confronted with his family's ancestral ban on music, enters the Land of the Dead.",
                            Director = "Lee Unkrich",
                            Genre = "Animation",
                            Language = 0,
                            Origincountry = "USA",
                            PosterImage = "coco.jpg",
                            Title = "Coco",
                            classification = 0,
                            duration = 105
                        },
                        new
                        {
                            FilmId = new Guid("116a23ce-afd5-4738-91bb-e4f3f7da6453"),
                            Cast = "Christian Bale, Heath Ledger, Aaron Eckhart",
                            Description = "Batman begins his fight against crime in Gotham.",
                            Director = "Christopher Nolan",
                            Genre = "Action",
                            Language = 0,
                            Origincountry = "USA",
                            PosterImage = "darkknight.jpg",
                            Title = "The Dark Knight",
                            classification = 2,
                            duration = 152
                        },
                        new
                        {
                            FilmId = new Guid("b6226872-2cc7-4fb7-b3c5-5aaf88397182"),
                            Cast = "Rumi Hiiragi, Miyu Irino, Mari Natsuki",
                            Description = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches, and spirits.",
                            Director = "Hayao Miyazaki",
                            Genre = "Animation",
                            Language = 0,
                            Origincountry = "Japan",
                            PosterImage = "spiritedaway.jpg",
                            Title = "Spirited Away",
                            classification = 1,
                            duration = 125
                        },
                        new
                        {
                            FilmId = new Guid("b98b7fd3-767e-4d0b-83c6-6f67053ac074"),
                            Cast = "Ivana Baquero, Ariadna Gil, Sergi López",
                            Description = "In the falangist Spain of 1944, the bookish young stepdaughter of a sadistic army officer escapes into an eerie but captivating fantasy world.",
                            Director = "Guillermo del Toro",
                            Genre = "Fantasy",
                            Language = 1,
                            Origincountry = "Spain",
                            PosterImage = "panslabyrinth.jpg",
                            Title = "Pan's Labyrinth",
                            classification = 3,
                            duration = 118
                        },
                        new
                        {
                            FilmId = new Guid("5785b4b7-8fe4-49fa-bd19-39e770b463b3"),
                            Cast = "Liam Neeson, Ralph Fiennes, Ben Kingsley",
                            Description = "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce.",
                            Director = "Steven Spielberg",
                            Genre = "Biography",
                            Language = 0,
                            Origincountry = "USA",
                            PosterImage = "schindlerslist.jpg",
                            Title = "Schindler's List",
                            classification = 4,
                            duration = 195
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
