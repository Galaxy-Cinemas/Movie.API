using AutoMapper;
using Galaxi.Bus.Message;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.DTOs;
using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MassTransit;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class CreatedMovieHandler
       : IRequestHandler<CreatedMovieCommand, CreatedFilmReponseDTO>
    
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreatedMovieHandler(IMovieRepository repo, IMapper mapper, IBus bus)
        {
            _repo = repo;
            _mapper = mapper;
            _bus = bus;
        }

        public async Task<CreatedFilmReponseDTO> Handle(CreatedMovieCommand request, CancellationToken cancellationToken)
        {
            var createdMovie = _mapper.Map<Film>(request);
            _repo.Add(createdMovie);
            
            var sucess = await _repo.SaveAll();
            var filmResponse = _mapper.Map<CreatedFilmReponseDTO>(createdMovie);

            await _bus.Publish(new AddMovie { film = createdMovie });

            if (!sucess)
            {
                throw new InvalidOperationException();
            }
            return filmResponse;
        }

    }
}
