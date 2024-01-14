using AutoMapper;
using Galaxi.Movie.Domain.DTOs;
using Galaxi.Movie.Domain.Infrastructure.Queries;
using Galaxi.Movie.Persistence.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Galaxi.Movie.Domain.Handlers
{
    public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<FilmDto>>
    {
        private readonly MovieContextDb _context;
        private readonly IMapper _mapper;

        public GetAllMoviesHandler(MovieContextDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FilmDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movie = await _context.Movie.ToListAsync(cancellationToken);

            var movieViewModel = _mapper.Map<List<FilmDto>>(movie);

            return movieViewModel;
        }
    }
}
