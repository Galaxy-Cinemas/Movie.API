using MediatR;

namespace Galaxi.Movie.Domain.Infrastructure.Commands
{
    public record UpdateMovieCommand (int MovieId, string Title, string Description, string Author, string Genre, string cast, string PosterImage)
        : IRequest<bool>;
}
