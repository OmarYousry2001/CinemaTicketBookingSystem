using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace MovieReservationSystem.Core.Features.Genres.Commands.Validators
{
    public class AddGenreValidator : AbstractValidator<AddGenreCommand>
    {
        private readonly IGenreService _genreService;

        public AddGenreValidator(IGenreService genreService)
        {
            _genreService = genreService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(g => g.NameAr)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));

            RuleFor(g => g.NameEn)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));
        }

        private void ApplyCustomValidationRules()
        {
            RuleFor(g => g.NameEn)
                .MustAsync(async (model, nameEn, cancellationToken) =>
                    !await _genreService.IsExistByNameAsync(nameEn, model.NameAr))
                .WithMessage(_ => SystemResources.NameAlreadyExists);
        }
    }
}
