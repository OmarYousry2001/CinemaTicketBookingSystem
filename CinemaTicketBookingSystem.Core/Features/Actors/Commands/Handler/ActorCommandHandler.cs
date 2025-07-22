using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Actors.Commands.Handler
{
    public class ActorCommandHandler : ResponseHandler
                                      , IRequestHandler<AddActorCommand, Response<string>>
                                      , IRequestHandler<EditActorCommand, Response<string>>
                                      , IRequestHandler<DeleteActorCommand, Response<string>>
    {
        #region Fields
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService; 
        #endregion

        #region Constructors
        public ActorCommandHandler(IActorService actorService, IMapper mapper, ICurrentUserService currentUserService) 
        {
            _actorService = actorService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddActorCommand request, CancellationToken cancellationToken)
        {
            var actor = _mapper.Map<Actor>(request);
            var isSaved = await _actorService.SaveAndUploadImageAsync(actor, _currentUserService.GetUserId(), request.Image);
            if (isSaved)
                return Success(ActionsResources.Accept);
            else
                 return NotFound<string>();
        }
        public async Task<Response<string>> Handle(EditActorCommand request, CancellationToken cancellationToken)
        {
            var oldActor = await _actorService.FindByIdAsync(request.Id);
            if(oldActor == null) return NotFound<string>();
            var mappedActor = _mapper.Map(request, oldActor);
            //Call service that make Edit
            var result = await _actorService.SaveAndUploadImageAsync(mappedActor, _currentUserService.GetUserId(), request.Image);
            
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            var actor = await _actorService.FindByIdAsync(request.Id);
            if (actor == null) return NotFound<string>();
            var isDeleted = await _actorService.DeleteAsync(actor);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();

        }
        #endregion

    }
}
