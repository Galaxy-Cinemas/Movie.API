using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.DTOs;
using Galaxi.Movie.Domain.Infrastructure.Queries;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Galaxi.Movie.Domain.Handlers
{
    public class GetMovieByIdHandler
         : IRequestHandler<GetMovieByIdQuery, FilmDto>
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

        public async Task<FilmDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Film movieById = await _repo.GetMovieById(request.movieId);
                var movieByIdViewModel = _mapper.Map<FilmDto>(movieById);
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
