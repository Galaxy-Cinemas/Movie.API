using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class UpdateMovieHandler
        : IRequestHandler<UpdateMovieCommand, bool>
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;

        public UpdateMovieHandler(IMovieRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var updateMovie = _mapper.Map<Film>(request);

            _repo.Update(updateMovie);

            return await _repo.SaveAll();
        }
    }
}
