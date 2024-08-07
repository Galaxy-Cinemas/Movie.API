using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class UpdateMovieHandler
        : IRequestHandler<UpdateMovieCommand, Unit>
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;

        public UpdateMovieHandler(IMovieRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var existingMovie = await _repo.GetMovieById(request.FilmId);
            if (existingMovie == null)
            {
                throw new KeyNotFoundException($"Movie not found.");
            }

            _mapper.Map(request, existingMovie);
            _repo.Update(existingMovie);

            var sucess = await _repo.SaveAll();
            if (!sucess)
            {
                throw new InvalidOperationException("Failed to save changes to the database.");
            }
            
            return Unit.Value;
        }
    }
}
