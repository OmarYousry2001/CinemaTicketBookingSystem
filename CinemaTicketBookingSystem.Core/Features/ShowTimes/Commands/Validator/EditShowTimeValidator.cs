using CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;


namespace CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Validator
{
    public class EditShowTimeValidator : AbstractValidator<EditShowTimeCommand>
    {
        private readonly IShowTimeService _showTimeService;
        private readonly IMovieService _movieService;
        private readonly IHallService _hallService;

        public EditShowTimeValidator(IShowTimeService showTimeService, IMovieService movieService, IHallService hallService)
        {
            _showTimeService = showTimeService;
            _movieService = movieService;
            _hallService = hallService;

            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(st => st.Day)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
                .WithMessage(_ => string.Format(ValidationResources.GreaterThan, DateOnly.FromDateTime(DateTime.Now)));

            RuleFor(st => st.StartTime)
     .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired);

            RuleFor(st => st.EndTime)
                 .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .GreaterThan(st => st.StartTime)
               .WithMessage(st => string.Format(ValidationResources.GreaterThan, st.StartTime));

            RuleFor(st => st.ShowTimePrice)
                .GreaterThanOrEqualTo(0).WithMessage(_ => string.Format(ValidationResources.GreaterThan, 0));

            RuleFor(st => st.HallId)
      .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired);

            RuleFor(st => st.MovieId)
      .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired);
        }
        private void ApplyCustomValidationRules()
        {
            //Check if Show Time Id is Valid
            RuleFor(st => st.Id)
                .MustAsync(async (key, CancellationToken) => await _showTimeService.IsExistAsync(key))
                .WithMessage(SystemResources.Invalid);

            //Check if ShowTime Exist in Same Hall and Same Day  and Time without itself
            RuleFor(st => st.StartTime).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _showTimeService.IsExistInSameHallExcludeItselfAsync(model.Id, model.Day, model.HallId, key, model.EndTime);
            }).WithMessage(SystemResources.ShowTimeOverlap);

            //Check if Movie Exist
            RuleFor(st => st.MovieId).MustAsync(async (key, CancellationToken) =>
            {
                return await _movieService.IsExistAsync(key);
            }).WithMessage(SystemResources.NotExist);

            //Check if Hall Exist
            RuleFor(st => st.HallId).MustAsync(async (key, CancellationToken) =>
            {
                return await _hallService.IsExistAsync(key);
            }).WithMessage(SystemResources.NotExist);
        }

    }
}
