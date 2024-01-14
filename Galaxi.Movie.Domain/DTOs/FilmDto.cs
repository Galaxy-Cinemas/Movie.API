namespace Galaxi.Movie.Domain.DTOs
{
    public class FilmDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Cast { get; set; }
        public string PosterImage { get; set; }
    }
}
