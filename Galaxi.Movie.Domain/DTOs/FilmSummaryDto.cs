﻿namespace Galaxi.Movie.Domain.DTOs
{
    public class FilmSummaryDto
    {
        public Guid filmId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string PosterImage { get; set; }
    }
}