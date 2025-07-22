using CinemaTicketBookingSystem.Core.Features.Reservations.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Reservations.Commands.Validators
{
    public class AddReservationValidator : AbstractValidator<AddReservationCommand>
    {
        private readonly IReservationService _reservationService;
        private readonly IApplicationUserService _userService;
        private readonly IShowTimeService _showTimeService;
        private readonly ISeatService _seatService;
        public AddReservationValidator(IReservationService reservationService, IApplicationUserService userService, IShowTimeService showTimeService, ISeatService seatService)
        {
            _reservationService = reservationService;
            _userService = userService;
            _showTimeService = showTimeService;
            _seatService = seatService;
            ApplyCustomRules();
        }

        private void ApplyCustomRules()
        {
            //Check If User Is Exist 
            RuleFor(r => r.UserId)
                .MustAsync(async (key, CancellationToken) => await _userService.FindByIdAsync(key) is not null)
                .WithMessage(UserResources.UserNotFound);

            //Check If ShowTime Is Exist and ShowTime is in the Available Future   
            RuleFor(r => r.ShowTimeId)
                .MustAsync(async (key, CancellationToken) => await _showTimeService.IsExistAndInFutureAsync(key))
                .WithMessage(SystemResources.EndedShowTime);

            //Check if Seats is Exist & Exist in the same  Hall
            RuleForEach(r => r.SeatIds).MustAsync(async (model, key, CancellationToken) =>
            {
                //Check if showTime is Exist and return HallId
                var hallId = _showTimeService.FindByIdAsync(model.ShowTimeId).Result.Hall.Id;
                return await _seatService.IsExistBySeatIdInHallAsync(key, hallId);
                // IsSeatExistAndInSameHallAsync

            }).WithMessage(SystemResources.SeatNotFoundInHall);

            //Check if Seats is Exist and Check of its availability
            RuleForEach(r => r.SeatIds).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _reservationService.IsSeatExistReservationInSameShowTimeAsync( model.ShowTimeId , key);

            }).WithMessage(SystemResources.SeatReserved);
        }
    }
}
