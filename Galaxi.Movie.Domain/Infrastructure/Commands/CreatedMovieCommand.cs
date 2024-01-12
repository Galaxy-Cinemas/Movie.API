using MediatR;

namespace Galaxi.Movie.Domain.Infrastructure.Commands
{
    public record CreatedMovieCommand(string Title, string Description)
        : IRequest<bool>;
    
}
