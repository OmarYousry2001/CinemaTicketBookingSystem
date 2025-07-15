using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace CinemaTicketBookingSystem.Core.Features.Actors.Queries.Handler
{
    public class ActorQueryHandler : ResponseHandler,
        IRequestHandler<GetAllActorsQuery, Response<List<GetAllActorsResponse>>>,
        IRequestHandler<FindActorsByIdQuery, Response<FindActorByIdResponse>>
    {
        #region Fields
        private readonly IActorService _actorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ActorQueryHandler(IActorService actorService, IMapper mapper)
        {
            _actorService = actorService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllActorsResponse>>> Handle(GetAllActorsQuery request, CancellationToken cancellationToken)
        {
            var entityList = await _actorService.GetAllAsync();

            var dtoList = _mapper.Map<List<GetAllActorsResponse>>(entityList);

            return Success(dtoList);
        }

        public async Task<Response<FindActorByIdResponse>> Handle(FindActorsByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _actorService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<FindActorByIdResponse>();

            var dto = _mapper.Map<FindActorByIdResponse>(entity);
            return Success<FindActorByIdResponse>(dto);

        }
    }
}
