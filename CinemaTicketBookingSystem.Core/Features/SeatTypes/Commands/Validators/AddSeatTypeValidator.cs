using CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Validators
{
    public class AddSeatTypeValidator : AbstractValidator<AddSeatTypeCommand>
    {
        private readonly ISeatTypeService _seatTypeService;

        public AddSeatTypeValidator(ISeatTypeService seatTypeService)
        {
            _seatTypeService = seatTypeService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(st => st.TypeNameAr)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));

            RuleFor(st => st.TypeNameEn)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));

            RuleFor(st => st.SeatTypePrice)
                .NotEmpty().WithMessage(_ => ValidationResources.FieldRequired)
                .GreaterThan(0).WithMessage(_ => string.Format(ValidationResources.GreaterThan, 0));
        }

        private void ApplyCustomValidationRules()
        {
            RuleFor(st => st.TypeNameEn)
                .MustAsync(async (model, typeNameEn, cancellationToken) =>
                    !await _seatTypeService.IsExistByNameAsync(typeNameEn, model.TypeNameAr))
                .WithMessage( SystemResources.NameAlreadyExists);
        }
    }
}
