using Galaxi.Movie.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Movie.Domain.DTOs
{
    public class FilmDetailsDto
    {
        public Guid filmId { get; set; }
        public string Title { get; set; }
        public string? PosterImage { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public string? Genre { get; set; }
        public string? Cast { get; set; }
        public string? Origincountry { get; set; }
        public string? Language { get; set; }
        public string? classification { get; set; }
        public int? duration { get; set; }
    }
}
