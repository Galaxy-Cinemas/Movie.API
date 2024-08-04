using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.DTOs;
using Galaxi.Movie.Domain.Infrastructure.Queries;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Galaxi.Movie.Domain.Handlers
{
    public class GetMovieByIdHandler
         : IRequestHandler<GetMovieByIdQuery, Film_DetailsDto>
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetMovieByIdHandler> _log;
        public GetMovieByIdHandler(IMovieRepository repo, IMapper mapper, ILogger<GetMovieByIdHandler> log)
        {
            _repo = repo;
            _mapper = mapper;
            _log = log;
        }

        public async Task<Film_DetailsDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Film movieById = await _repo.GetMovieById(request.filmId);
                var movieByIdViewModel = _mapper.Map<Film_DetailsDto>(movieById);
                return movieByIdViewModel;
            }
            catch (Exception ex)
            {
                _log.LogError("An exception has occurred getting the movie{0}", ex.Message);
                throw;
            }
        }
    }
}
