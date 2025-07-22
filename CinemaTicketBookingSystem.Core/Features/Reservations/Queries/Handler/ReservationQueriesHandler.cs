using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using SchoolProject.Core.Wrappers;

namespace MovieReservationSystem.Core.Features.Reservations.Queries.Handler
{
    internal class ReservationQueriesHandler : ResponseHandler,
        IRequestHandler<GetReservationsPaginatedListQuery, PaginatedResult<GetReservationsPaginatedListResponse>>,
        IRequestHandler<FindReservationByIdQuery, Response<FindReservationByIdResponse>>
    {
        #region Fields
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public ReservationQueriesHandler(IReservationService reservationService, IMapper mapper)
        {
            _reservationService = reservationService;
            _mapper = mapper;
        }
        #endregion

        #region Handlers
        public async Task<Response<FindReservationByIdResponse>> Handle(FindReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationService.FindByIdAsync(request.Id);
            if (reservation is null)
                return NotFound<FindReservationByIdResponse>();

            var mappedReservation = _mapper.Map<FindReservationByIdResponse>(reservation);

            return Success(mappedReservation);
        }

        public async Task<PaginatedResult<GetReservationsPaginatedListResponse>> Handle(GetReservationsPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var FilterQuery = _reservationService.GetAllQueryable(request.Search);
            var PaginatedList = await _mapper.ProjectTo<GetReservationsPaginatedListResponse>(FilterQuery)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }
        #endregion
    }
}
