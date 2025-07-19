using CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;


namespace CinemaTicketBookingSystem.Core.Features.Seats.Commands.Validator
{
    public class EditSeatValidator : AbstractValidator<EditSeatCommand>
    {
        private readonly ISeatService _seatService;
        private readonly IHallService _hallService;
        private readonly ISeatTypeService _seatTypeService;

        public EditSeatValidator(ISeatService seatService, IHallService hallService, ISeatTypeService seatTypeService)
        {
            _seatService = seatService;
            _hallService = hallService;
            _seatTypeService = seatTypeService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {

            RuleFor(s => s.SeatNumber)
             .NotEmpty().WithMessage(ValidationResources.FieldRequired)
              .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
             .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));
        }
        private void ApplyCustomValidationRules()
        {
            //Check if SeatNumber is  Exist in Hall Exclude Itself
            RuleFor(s => s.SeatNumber).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _seatService.IsExistInHallExcludeItselfAsync(model.Id ,key, model.HallId);
            }).WithMessage(SystemResources.AlreadyExists);

            //Check if Hall is Not Exist
            RuleFor(s => s.HallId).MustAsync(async (key, CancellationToken) =>
            {
                return await _hallService.IsExistAsync(key);
            }).WithMessage(SystemResources.NotExist);

            //Check if SeatType is Not Exist
            RuleFor(s => s.SeatTypeId).MustAsync(async (key, CancellationToken) =>
            {
                return await _seatTypeService.IsExistAsync(key);
            }).WithMessage(SystemResources.NotExist);

            // Validates that the hall has reached its maximum seating capacity
            RuleFor(s => s.HallId).MustAsync(async (key, CancellationToken) =>
            {
                var hall = await _hallService.FindByIdAsync(key);
                if (hall == null) return false;
                var seatsCountInHall = await _seatService.CountSeatsInHall(hall.Id);
                return seatsCountInHall < hall.Capacity;
            }).WithMessage(SystemResources.HallCapacityReached);

        }
    }
}
