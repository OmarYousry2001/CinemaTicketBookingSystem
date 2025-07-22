using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Reservations.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MovieReservationSystem.Core.Features.Reservations.Commands.Handler
{
    public class ReservationCommandsHandler : ResponseHandler,
        IRequestHandler<AddReservationCommand, Response<string>>,
        IRequestHandler<DeleteReservationCommand, Response<string>>
    {
        #region Fields
        private readonly IReservationService _reservationService;
        private readonly IApplicationUserService _userService;
        private readonly IShowTimeService _showTimeService;
        private readonly ISeatService _seatService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors
        public ReservationCommandsHandler(IReservationService reservationService
            , IApplicationUserService userService
            , IShowTimeService showTimeService
            , ISeatService seatService
            , IMapper mapper
            , ICurrentUserService currentUserService)

        {
            _reservationService = reservationService;
            _userService = userService;
            _showTimeService = showTimeService;
            _seatService = seatService;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        #endregion
        public async Task<Response<string>> Handle(AddReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            var showTime = await _showTimeService.FindByIdAsync(request.ShowTimeId);
            var userId = _currentUserService.GetUserId();
            var seatsList = _seatService.GetAllQueryable().Include(x => x.SeatType)
                .Where(u => request.SeatIds.Contains(u.Id)).ToList();


            var SeatIds = request.SeatIds.Select(seatId => new Seat { Id = seatId }).ToList();

            var newReservation = new Reservation()
            {
                ReservationSeats = request.SeatIds.Select(seatId => new ReservationSeat { SeatId = seatId }).ToList(),
                FinalPrice = _reservationService.CalculateReservationPrice(seatsList, showTime.ShowTimePrice),
                ShowTime = showTime,
                UserId = userId.ToString()
            };

            var createdReservation = await _reservationService.AddAsync(newReservation, userId);

            if (createdReservation) return Created<string>(ActionsResources.Accept);
            else return BadRequest<string>();
        }
        public async Task<Response<string>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _reservationService.FindByIdAsync(request.Id);
            if (reservation == null) return NotFound<string>();

            if (reservation.ShowTime.Day.Day <= DateTime.Now.Day)
                return BadRequest<string>(SystemResources.CannotCancelReservation);

            var isDeleted = await _reservationService.DeleteAsync(reservation);
            return isDeleted ? Deleted<string>() : BadRequest<string>();
        }
    }
}
