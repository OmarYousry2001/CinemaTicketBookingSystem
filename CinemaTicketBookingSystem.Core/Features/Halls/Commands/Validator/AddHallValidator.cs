using CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Halls.Commands.Validator
{
    public class AddHallValidator : AbstractValidator<AddHallCommand>
    {
        private readonly IHallService _hallService;

        public AddHallValidator(IHallService hallService)
        {
            _hallService = hallService;

            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(h => h.NameAr)
                .NotEmpty()
                    .WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2)
                    .WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100)
                    .WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));

            RuleFor(h => h.NameEn)
                .NotEmpty()
                    .WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2)
                    .WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100)
                    .WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));

            RuleFor(h => h.Capacity)
                .GreaterThan(0)
                    .WithMessage(_ => string.Format(ValidationResources.GreaterThan, 0));
        }

        private void ApplyCustomValidationRules()
        {
            RuleFor(h => h.NameEn)
                .MustAsync(async (model, nameEn, cancellationToken) =>
                    !await _hallService.IsExistByNameAsync(nameEn, model.NameAr))
                .WithMessage(_ =>  SystemResources.NameAlreadyExists);
        }
    }
}
