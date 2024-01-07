using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.DTOs;
using Galaxi.Movie.Domain.Infrastructure.Commands;

using Galaxi.Movie.Persistence.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Galaxi.Movie.Domain.Handlers
{
    public class CreatedMovieHandler
       : IRequestHandler<CreatedMovieCommand, FilmCreatedDto>
    
    {
        private readonly MovieContextDb _dbcontext;
        private readonly IMapper _mapper;

        public CreatedMovieHandler(MovieContextDb dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public Task<IEnumerable<FilmDto>> Handle(CreatedMovieCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<FilmCreatedDto> IRequestHandler<CreatedMovieCommand, FilmCreatedDto>.Handle(CreatedMovieCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
