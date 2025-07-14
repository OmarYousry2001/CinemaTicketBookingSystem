using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;
using SchoolProject.Core.Resources;


namespace CinemaTicketBookingSystem.Core.Features.Actors.Commands.Validator
{
    public class CreateActorValidator : AbstractValidator<AddActorCommand>
    {
        private readonly IActorService _actorService;

        public CreateActorValidator(IActorService actorService)
        {
            _actorService = actorService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(a => a.FirstName)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                //.NotNull().WithMessage(SharedResourcesKeys.NotNull)
                .MaximumLength(100).WithMessage(string.Format(ValidationResources.MaxLengthExceeded, 100));


            RuleFor(a => a.LastName)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                               //.NotNull().WithMessage(SharedResourcesKeys.NotNull)
                               .MaximumLength(100).WithMessage(string.Format(ValidationResources.MaxLengthExceeded, 100));


            RuleFor(a => a.Bio)
       .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                //.NotNull().WithMessage(SharedResourcesKeys.NotNull)
     .MaximumLength(500).WithMessage(string.Format(ValidationResources.MaxLengthExceeded, 500));


            //RuleFor(a => a.BirthDate)
            //  .NotEmpty().WithMessage(ValidationResources.FieldRequired)
            //    //.NotNull().WithMessage(SharedResourcesKeys.NotNull)
            //    .GreaterThan(new DateOnly(1500, 1, 1)).WithMessage($"{SharedResourcesKeys.GreaterThan} 1-1-1900");


            RuleFor(a => a.BirthDate)
     .NotEmpty().WithMessage(ValidationResources.FieldRequired)
    //.NotNull().WithMessage(SharedResourcesKeys.NotNull)
    .GreaterThan(new DateOnly(1800, 1, 1)).WithMessage(string.Format( ValidationResources.GreaterThan ,"1-1-1800"))
    .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today)).WithMessage(SystemResources.LessThanOrEqualToToday);


       



        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(a => a.Image)
           .NotEmpty().WithMessage(ValidationResources.FieldRequired)
           .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
           .WithMessage(_ => string.Format(ValidationResources.FileSizeLimit, 5))
           .Must(file =>
           {
               if (file == null) return true;

               var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/webp" };
               var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

               var contentTypeOk = allowedContentTypes.Contains(file.ContentType.ToLower());
               var extension = Path.GetExtension(file.FileName)?.ToLower();
               var extensionOk = allowedExtensions.Contains(extension);

               return contentTypeOk && extensionOk;
           })
           .WithMessage(_ => ValidationResources.InvalidImageExtension);
            RuleFor(a => a.FirstName).MustAsync(async (model, key, CancellationToken) =>
            {
                return !await _actorService.IsExistByNameAsync(key, model.LastName);
            }).WithMessage(SystemResources.NameAlreadyExists);
        }
    }
}
