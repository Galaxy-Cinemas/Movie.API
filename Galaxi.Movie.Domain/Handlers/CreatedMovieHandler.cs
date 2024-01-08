using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.DTOs;
using Galaxi.Movie.Domain.Infrastructure.Commands;

using Galaxi.Movie.Persistence.Persistence;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class CreatedMovieHandler
       : IRequestHandler<CreatedMovieCommand, bool>
    
    {
        private readonly MovieRepository _repo;
        private readonly IMapper _mapper;

        public CreatedMovieHandler(MovieRepository dbcontext, IMapper mapper)
        {
            _repo = dbcontext;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreatedMovieCommand request, CancellationToken cancellationToken)
        {
            var movieViewModel = _mapper.Map<Film>(request);

            _repo.Add(movieViewModel);

            return await _repo.SaveAll();
        }
    }
}
