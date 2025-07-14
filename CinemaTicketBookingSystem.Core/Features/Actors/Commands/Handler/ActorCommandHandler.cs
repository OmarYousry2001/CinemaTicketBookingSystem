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
    public class ActorCommandHandler: ResponseHandler
                                      ,IRequestHandler<AddActorCommand, Response<string>>   
    {
        #region Fields
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;
        //private readonly IFileService _fileService;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion
        #region Constructors
        public ActorCommandHandler(IActorService actorService, IMapper mapper , IStringLocalizer<SharedResources> localizer): base(localizer)  
        {
            _actorService = actorService;
            _mapper = mapper;
            _localizer = localizer;
        //_fileService = fileService;
        //_contextAccessor = contextAccessor;
    }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(AddActorCommand request, CancellationToken cancellationToken)
        {


            var entity = _mapper.Map<Actor>(request);
            var savedActor = await _actorService.AddAsync(entity , Guid.NewGuid());
            if(savedActor)
            return Success(ActionsResources.Accept);
            else
                return NotFound<string>("not found error" );





        }

        #endregion

    }
}
