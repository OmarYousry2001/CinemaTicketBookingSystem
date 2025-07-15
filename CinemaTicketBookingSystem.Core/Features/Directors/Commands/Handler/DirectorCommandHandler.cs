using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models;
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
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public DirectorCommandHandler(IDirectorService actorService, IMapper mapper, IStringLocalizer<SharedResources> localizer)
        {
            _directorService = actorService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddDirectorCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Director>(request);
            var savedActor = await _directorService.SaveAndUploadImageAsync(entity, Guid.NewGuid(), request.Image);
            if (savedActor)
                return Success(ActionsResources.Accept);
            else
                return NotFound<string>();
        }
        public async Task<Response<string>> Handle(EditDirectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _directorService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            //mapping Between request and student
            var entityMapping = _mapper.Map(request, entity);
            var result = await _directorService.SaveAndUploadImageAsync(entityMapping, Guid.NewGuid(), request.Image);

            //return response
            if (result) return Success(NotifiAndAlertsResources.ItemUpdated);
            else return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await _directorService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<string>();
            var isDeleted = await _directorService.Delete(entity);
            if (isDeleted) return Deleted<string>();
            else return BadRequest<string>();
        }
        #endregion

    }
}
