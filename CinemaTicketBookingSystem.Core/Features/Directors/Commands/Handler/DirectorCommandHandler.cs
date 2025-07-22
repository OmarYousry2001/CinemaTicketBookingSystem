using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Directors.Commands.Handler
{
    public class DirectorCommandHandler : ResponseHandler
                                      , IRequestHandler<AddDirectorCommand, Response<string>>
                                      , IRequestHandler<EditDirectorCommand, Response<string>>
                                      , IRequestHandler<DeleteDirectorCommand, Response<string>>
    {
        #region Fields
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        #endregion

        #region Constructors
        public DirectorCommandHandler(IDirectorService actorService, IMapper mapper
            , ICurrentUserService currentUserService)
        {
            _directorService = actorService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = _mapper.Map<Director>(request);
            var savedDirectorr = await _directorService.SaveAndUploadImageAsync(director, _currentUserService.GetUserId(), request.Image);
            if (savedDirectorr)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();
        }
        public async Task<Response<string>> Handle(EditDirectorCommand request, CancellationToken cancellationToken)
        {
            var oldDirector = await _directorService.FindByIdAsync(request.Id);
            if (oldDirector == null) return NotFound<string>();
            //mapping Between request and student
            var mappedDirector = _mapper.Map(request, oldDirector);
            var result = await _directorService.SaveAndUploadImageAsync(mappedDirector, _currentUserService.GetUserId(), request.Image);

            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var director = await _directorService.FindByIdAsync(request.Id);
            if (director == null) return NotFound<string>();
            var isDeleted = await _directorService.DeleteAsync(director);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        }
        #endregion

    }
}
