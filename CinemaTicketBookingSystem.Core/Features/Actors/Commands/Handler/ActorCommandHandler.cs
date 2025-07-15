using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;


using SchoolProject.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public ActorCommandHandler(IActorService actorService, IMapper mapper, IStringLocalizer<SharedResources> localizer) 
        {
            _actorService = actorService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddActorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Actor>(request);
            var isSaved = await _actorService.SaveAndUploadImageAsync(entity, Guid.NewGuid(), request.Image);
            if (isSaved)
                return Success(ActionsResources.Accept);
            else
                 return NotFound<string>();


        }
        public async Task<Response<string>> Handle(EditActorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _actorService.FindByIdAsync(request.Id);
            if(entity == null) return NotFound<string>();
            var entityMapping = _mapper.Map(request, entity);
            //Call service that make Edit
            var result = await _actorService.SaveAndUploadImageAsync(entityMapping, Guid.NewGuid() ,request.Image);

            
            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();


            
        }
        public async Task<Response<string>> Handle(DeleteActorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _actorService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var isDeleted = await _actorService.Delete(entity);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();

        }
        #endregion

    }
}
