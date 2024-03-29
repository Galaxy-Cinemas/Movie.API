﻿using Galaxi.Movie.Domain.Infrastructure.Commands;
using Galaxi.Movie.Persistence.Repositorys;
using MediatR;

namespace Galaxi.Movie.Domain.Handlers
{
    public class DeletedMovieHandler
        : IRequestHandler<DeleteMovieCommand, bool>
    {
        private readonly IMovieRepository _repo;

        public DeletedMovieHandler(IMovieRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var function = await _repo.GetMovieById(request.movieId);
            _repo.Delete(function);
            return await _repo.SaveAll();
        }
    }
}
