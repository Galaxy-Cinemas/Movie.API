using Galaxi.Movie.Domain.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Movie.Domain.Infrastructure.Commands
{
    public record UpdateMovieCommand (int movieId, string Description)
        : IRequest<FilmUpdateDto>;
}
