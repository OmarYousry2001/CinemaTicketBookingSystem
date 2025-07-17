using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Implementations;
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
        #endregion

        #region Constructors
        public GenreCommandsHandler(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }
        #endregion

        public async Task<Response<string>> Handle(AddGenreCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Genre>(request);
            var savedActor = await _genreService.SaveAsync(entity, Guid.NewGuid());
            if (savedActor)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();
        }
        public async Task<Response<string>> Handle(EditGenreCommand request, CancellationToken cancellationToken)
        {
            var entity = await _genreService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var entityMapping = _mapper.Map(request, entity);
            var result = await _genreService.SaveAsync(entityMapping, Guid.NewGuid());
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var entity = await _genreService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var isDeleted = await _genreService.DeleteAsync(entity);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        }
    }
}
