using Galaxi.Bus.Message;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MassTransit;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class DeletedMovieHandler
        : IRequestHandler<DeleteMovieCommand, Unit>
    {
        private readonly IMovieRepository _repo;
        private readonly IBus _bus;

        public DeletedMovieHandler(IMovieRepository repo, IBus bus)
        {
            _repo = repo;
            _bus = bus;
        }
        public async Task<Unit> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var existingMovie = await _repo.GetMovieByIdAsync(request.FilmId);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException();   
            }

            await _repo.Delete(existingMovie);
            await _bus.Publish(new DeleteMovie { film = existingMovie });
            var sucess = await _repo.SaveAll();

            if (!sucess)
            {
                throw new InvalidOperationException();
            }

            return Unit.Value;
        }
    }
}
