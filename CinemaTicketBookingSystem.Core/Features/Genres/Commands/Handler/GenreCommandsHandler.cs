using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Genres.Commands.Handler
{
    public class GenreCommandsHandler : ResponseHandler,
        IRequestHandler<AddGenreCommand, Response<string>>,
        IRequestHandler<EditGenreCommand, Response<string>>,
        IRequestHandler<DeleteGenreCommand, Response<string>>
    {
        #region Fields
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public GenreCommandsHandler(IGenreService genreService, IMapper mapper, ICurrentUserService currentUserService)
        {
            _genreService = genreService;
            _mapper = mapper;
            _currentUserService = currentUserService;

        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = _mapper.Map<Genre>(request);
            var savedGenre = await _genreService.SaveAsync(genre, _currentUserService.GetUserId());
            if (savedGenre)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();
        }
        public async Task<Response<string>> Handle(EditGenreCommand request, CancellationToken cancellationToken)
        {
            var oldGenre = await _genreService.FindByIdAsync(request.Id);
            if (oldGenre == null) return NotFound<string>();
            var mappedGenre = _mapper.Map(request, oldGenre);
            var result = await _genreService.SaveAsync(mappedGenre, _currentUserService.GetUserId());
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreService.FindByIdAsync(request.Id);
            if (genre == null) return NotFound<string>();
            var isDeleted = await _genreService.DeleteAsync(genre);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        } 
        #endregion
    }
}
