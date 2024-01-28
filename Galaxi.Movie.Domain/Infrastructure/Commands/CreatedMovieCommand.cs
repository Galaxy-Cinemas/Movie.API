using MediatR;

namespace Galaxi.Movie.Domain.Infrastructure.Commands
{
    public record CreatedMovieCommand(string Title, string Description, string Author, string Genre, string Cast, string PosterImage)
        : IRequest<bool>;

}
