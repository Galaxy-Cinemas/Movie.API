using AutoMapper;
using Galaxi.Bus.Message;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MassTransit;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class UpdateMovieHandler
        : IRequestHandler<UpdateMovieCommand, Unit>
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UpdateMovieHandler(IMovieRepository repo, IMapper mapper, IBus bus)
        {
            _repo = repo;
            _mapper = mapper;
            _bus = bus;
        }
        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var existingMovie = await _repo.GetMovieByIdAsync(request.FilmId);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException();
            }

            _mapper.Map(request, existingMovie);
            _repo.Update(existingMovie);

            await _bus.Publish(new UpdateMovie { film = existingMovie });

            var sucess = await _repo.SaveAll();
            if (!sucess)
            {
                throw new InvalidOperationException();
            }
            
            return Unit.Value;
        }
    }
}
