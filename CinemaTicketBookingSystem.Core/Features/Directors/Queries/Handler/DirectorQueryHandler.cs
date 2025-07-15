using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Resources;

namespace CinemaTicketBookingSystem.Core.Features.Directors.Queries.Handler
{
    public class DirectorQueryHandler : ResponseHandler,
        IRequestHandler<GetAllDirectorsQuery, Response<List<GetAllDirectorsResponse>>>,
        IRequestHandler<FindDirectorByIdQuery, Response<FindDirectorByIdResponse>>
    {
        #region Fields
        private readonly IDirectorService _directorService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DirectorQueryHandler(IDirectorService directorService, IMapper mapper)
        {
            _directorService = directorService;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetAllDirectorsResponse>>> Handle(GetAllDirectorsQuery request, CancellationToken cancellationToken)
        {
            var actorsList = await _directorService.GetAllAsync();

            var mappedActorsList = _mapper.Map<List<GetAllDirectorsResponse>>(actorsList);

            return Success(mappedActorsList);
        }

        public async Task<Response<FindDirectorByIdResponse>> Handle(FindDirectorByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _directorService.FindByIdAsync(request.Id);
            if (entity == null) return NotFound<FindDirectorByIdResponse>();

            var dto = _mapper.Map<FindDirectorByIdResponse>(entity);
            return Success<FindDirectorByIdResponse>(dto);

        }
    }
}
