using AutoMapper;
using Galaxi.Movie.Data.Models;
using Galaxi.Movie.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxi.Movie.Domain.Profiles
{
    public class MovieDtosProfile : Profile
    {
        public MovieDtosProfile()
        {
            CreateMap<Film, FilmDto>();

            CreateMap<FilmCreatedDto, Film>();

            CreateMap<FilmUpdateDto, Film>();
        }
    }
}
