using MediatR;

namespace Galaxi.Movie.Domain.Infrastructure.Commands
{
    public record DeleteMovieCommand(int id) 
        : IRequest<bool>;
    
}
