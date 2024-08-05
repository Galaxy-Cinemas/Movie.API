using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class DeletedMovieHandler
        : IRequestHandler<DeleteMovieCommand, Unit>
    {
        private readonly IMovieRepository _repo;

        public DeletedMovieHandler(IMovieRepository repo)
        {
            _repo = repo;
        }
        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var function = await _repo.GetMovieById(request.FilmId);
            if (function == null)
            {
                throw new DirectoryNotFoundException($"Movie with ID {request.FilmId} not found.");
                
            }

            _repo.Delete(function);
            var sucess = await _repo.SaveAll();
            if (sucess)
            {
                throw new Exception("Failed to save changes.");
            }

            return Unit.Value;
        }
    }
}
