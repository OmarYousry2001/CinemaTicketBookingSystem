using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Seats.Queries.Handler
{
    public class SeatQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSeatsQuery, Response<List<GetAllSeatsResponse>>>,
        //IRequestHandler<GetFreeSeatsInShowTimeQuery, Response<List<GetFreeSeatsInShowTimeResponse>>>,
        IRequestHandler<FindSeatByIdQuery, Response<FindSeatByIdResponse>>
    {
        #region Fields
        private readonly ISeatService _seatService;
        //private readonly IReservationService _reservationService;
        private readonly IShowTimeService _showTimeService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public SeatQueryHandler(ISeatService seatService, IMapper mapper, IShowTimeService showTimeService)
        {
            _seatService = seatService;
            _mapper = mapper;
            //_reservationService = reservationService;
            _showTimeService = showTimeService;
        }
        #endregion
        public async Task<Response<List<GetAllSeatsResponse>>> Handle(GetAllSeatsQuery request, CancellationToken cancellationToken)
        {
            var seatsList = await _seatService.GetAllAsync();
            var mappedSeatsList = _mapper.Map<List<GetAllSeatsResponse>>(seatsList);
            return Success(mappedSeatsList);
        }

        public async Task<Response<FindSeatByIdResponse>> Handle(FindSeatByIdQuery request, CancellationToken cancellationToken)
        {
            var seat = await _seatService.FindByIdAsync(request.Id);

            if (seat is null)
                return NotFound<FindSeatByIdResponse>();
            var mappedSeat = _mapper.Map<FindSeatByIdResponse>(seat);
            return Success(mappedSeat);
        }

        //public async Task<Response<List<GetFreeSeatsInShowTimeResponse>>> Handle(GetFreeSeatsInShowTimeQuery request, CancellationToken cancellationToken)
        //{
        //    var showTime = await _showTimeService.GetByIdAsync(request.ShowTimeId);
        //    if (showTime is null)
        //        return BadRequest<List<GetFreeSeatsInShowTimeResponse>>(SharedResourcesKeys.Invalid);

        //    var hallId = showTime.Hall.HallId;

        //    await _reservationService.DeleteNotCompletedReservations();

        //    var freeSeatsList = await _seatService.GetAllQueryable()
        //        .Where(s => !s.Reservations.Any(r => r.ShowTime.ShowTimeId == request.ShowTimeId) &&
        //                    s.Hall.HallId == hallId)
        //        .Select(s => new GetFreeSeatsInShowTimeResponse
        //        {
        //            SeatId = s.SeatId,
        //            SeatNumber = s.SeatNumber,
        //            SeatTypeName = s.SeatType.TypeName
        //        })
        //        .ToListAsync();

        //    return Success(freeSeatsList);
        //}
    }
}
