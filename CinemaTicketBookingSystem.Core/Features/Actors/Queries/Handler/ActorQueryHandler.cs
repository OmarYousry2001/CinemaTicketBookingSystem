using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

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

        #region Handle Functions
        public async Task<Response<List<GetAllActorsResponse>>> Handle(GetAllActorsQuery request, CancellationToken cancellationToken)
        {
            var actorList = await _actorService.GetAllAsync();

            var mappedActorList = _mapper.Map<List<GetAllActorsResponse>>(actorList);

            return Success(mappedActorList);
        }

        public async Task<Response<FindActorByIdResponse>> Handle(FindActorsByIdQuery request, CancellationToken cancellationToken)
        {
            var actor = await _actorService.FindByIdAsync(request.Id);
            if (actor == null) return NotFound<FindActorByIdResponse>();

            var mappedActor = _mapper.Map<FindActorByIdResponse>(actor);
            return Success<FindActorByIdResponse>(mappedActor);

        } 
        #endregion
    }
}
