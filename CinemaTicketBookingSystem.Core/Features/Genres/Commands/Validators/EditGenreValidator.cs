using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Genres.Commands.Validators
{
    public class EditGenreValidator : AbstractValidator<EditGenreCommand>
    {
        private readonly IGenreService _genreService;

        public EditGenreValidator(IGenreService genreService)
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
                    !await _genreService.IsExistByNameExcludeItselfAsync(model.Id, nameEn, model.NameAr))
                .WithMessage(_ => SystemResources.NameAlreadyExists);
        }
    }
}
