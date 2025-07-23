using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CinemaTicketBookingSystem.Core.Features.Seats.Queries.Handler
{
    public class SeatQueryHandler : ResponseHandler,
        IRequestHandler<GetAllSeatsQuery, Response<List<GetAllSeatsResponse>>>,
        IRequestHandler<GetFreeSeatsInShowTimeQuery, Response<List<GetFreeSeatsInShowTimeResponse>>>,
        IRequestHandler<FindSeatByIdQuery, Response<FindSeatByIdResponse>>
    {
        #region Fields
        private readonly ISeatService _seatService;
        private readonly IReservationService _reservationService;
        private readonly IShowTimeService _showTimeService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors
        public SeatQueryHandler(ISeatService seatService
            , IMapper mapper
            , IShowTimeService showTimeService 
            , IReservationService reservationService)
        {
            _seatService = seatService;
            _mapper = mapper;
            _reservationService = reservationService;
            _showTimeService = showTimeService;
        }
        #endregion

        #region Handle Functions
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

        /// <summary>
        /// Retrieves all available (free) seats for a specific show time.
        /// It ensures that expired and unpaid reservations are deleted first,
        /// then fetches all seats in the corresponding hall that are not reserved 
        /// for the specified show time.
        /// </summary>
        /// <param name="query">The query containing the target ShowTimeId.</param>
        /// <param name="cancellationToken">Token to cancel the operation if needed.</param>
        /// <returns>
        /// A response wrapping a list of seats that are free during the given show time,
        /// each including seat ID, number, and seat type name.
        /// </returns>
        public async Task<Response<List<GetFreeSeatsInShowTimeResponse>>> Handle(GetFreeSeatsInShowTimeQuery request, CancellationToken cancellationToken)
        {
            var showTime = await _showTimeService.FindByIdAsync(request.ShowTimeId);
            if (showTime is null)
                return NotFound<List<GetFreeSeatsInShowTimeResponse>>();

            var hallId = showTime.Hall.Id;

            await _reservationService.DeleteExpiredUnpaidReservationsAsync();

                            var freeSeatsList = await _seatService.GetAllQueryable()
                           .Include(x => x.Hall)
                           .Include(x => x.ReservationSeats).ThenInclude(rs => rs.Reservation).ThenInclude(r => r.ShowTime)
                           .Where(s => !s.ReservationSeats.Any(r => r.Reservation.ShowTime.Id == request.ShowTimeId) &&
                            s.Hall.Id == hallId)

                .Select(s => new GetFreeSeatsInShowTimeResponse
                {
                    Id = s.Id,
                    SeatNumber = s.SeatNumber,
                    SeatTypeName = s.SeatType.Localize(s.SeatType.TypeNameAr, s.SeatType.TypeNameEn),   
                })
                .ToListAsync();

            return Success(freeSeatsList);
        }
        #endregion
    }
}
