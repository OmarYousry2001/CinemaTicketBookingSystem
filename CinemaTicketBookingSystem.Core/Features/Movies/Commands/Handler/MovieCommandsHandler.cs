using AutoMapper;
using Azure.Core;
using CinemaTicketBookingSystem.Core.Features.Movies.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace CinemaTicketBookingSystem.Core.Features.Movies.Commands.Handler
{
    public class MovieCommandsHandler : ResponseHandler,
        IRequestHandler<AddMovieCommand, Response<string>>,
        IRequestHandler<EditMovieCommand, Response<string>>,
        IRequestHandler<DeleteMovieCommand, Response<string>>
    {
        #region Fields
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IDirectorService _directorService;
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors
        public MovieCommandsHandler(IMovieService movieService
            , IMapper mapper
            , IGenreService genreService
            , IDirectorService directorService
            , IActorService actorService)
        {
            _movieService = movieService;
            _mapper = mapper;
            _genreService = genreService;
            _directorService = directorService;
            _actorService = actorService;

        }
        #endregion
        public async Task<Response<string>> Handle(AddMovieCommand request, CancellationToken cancellationToken)
        {
            var newMovie = _mapper.Map<Movie>(request);

            newMovie.MovieGenres = request.GenresIds.Distinct().Select(genreId => new MovieGenre
            {
                GenreId = genreId
            }).ToList();

            newMovie.MovieActors = request.ActorsIds.Distinct().Select(actorId => new MovieActor
            {
                ActorId = actorId
            }).ToList();

            newMovie.DirectorId = request.DirectorId;
            var createdMovie = await _movieService.SaveMovieWithRelationsAsync(newMovie, Guid.NewGuid(), request.Poster);

            if (createdMovie)
                return Created<string>("createted");
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditMovieCommand request, CancellationToken cancellationToken)
        {
            var oldMovie = await _movieService.FindByIdAsync(request.Id);

            var mappedMovie = _mapper.Map(request, oldMovie);


            // check if ids for genres and actors are Exist in the database    
            var genres = _genreService.GetAllQueryable().Where(g => request.GenresIds.Contains(g.Id)).ToList();
            var actors = _actorService.GetAllQueryable().Where(a => request.ActorsIds.Contains(a.Id)).ToList();

            if (genres.Count != request.GenresIds.Count)
                return BadRequest<string>("Some genres do not exist");
            if (actors.Count != request.ActorsIds.Count)
                return BadRequest<string>("Some actors do not exist");  


            var director = await _directorService.FindByIdAsync(request.DirectorId);

            //request.Title = request.Title.Trim();

            // the mappedMovie with the new values  
            mappedMovie.MovieGenres = genres.Select(genre => new MovieGenre { GenreId = genre.Id }).ToList();
            mappedMovie.Director = director;
            mappedMovie.MovieActors = actors.Select(actor => new MovieActor { ActorId = actor.Id }).ToList();




            var editedMovie = await _movieService.SaveMovieWithRelationsAsync(mappedMovie, Guid.NewGuid(), request.Poster);


            if (editedMovie)
                return Success("edited");
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var movie = await _movieService.FindByIdAsync(request.Id);

            var isDeleted = await _movieService.DeleteAsync(movie );
            if (isDeleted)
                return Deleted<string>();
            else
                return BadRequest<string>();
        }
    }
}
