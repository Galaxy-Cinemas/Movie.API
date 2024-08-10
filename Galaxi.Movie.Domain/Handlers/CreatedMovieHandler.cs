using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class CreatedMovieHandler
       : IRequestHandler<CreatedMovieCommand, Unit>
    
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;

        public CreatedMovieHandler(IMovieRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreatedMovieCommand request, CancellationToken cancellationToken)
        {
            var createdMovie = _mapper.Map<Film>(request);
            _repo.Add(createdMovie);
            var sucess = await _repo.SaveAll();

            if (!sucess)
            {
                throw new InvalidOperationException();
            }
            return Unit.Value;
        }

    }
}
