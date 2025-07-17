using CinemaTicketBookingSystem.Core.Features.Movies.Commands.Models;
using CinemaTicketBookingSystem.Data.Enums;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;



namespace CinemaTicketBookingSystem.Core.Features.Movies.Commands.Validators
{
    public class EditMovieCommandValidator : AbstractValidator<EditMovieCommand>
    {
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IActorService _actorService;
        public EditMovieCommandValidator(IGenreService genreService, IMovieService movieService, IDirectorService directorService, IActorService actorService)
        {
            _genreService = genreService;
            _movieService = movieService;
            _directorService = directorService;
            _actorService = actorService;
            ApplyCustomRules();
            ApplyValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(m => m.TitleAr)
             .NotEmpty().WithMessage(ValidationResources.FieldRequired)
              .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
             .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));


            RuleFor(m => m.DescriptionAr)
.NotEmpty().WithMessage(ValidationResources.FieldRequired)
 .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
.MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 1000));



            RuleFor(m => m.DescriptionEn)
.NotEmpty().WithMessage(ValidationResources.FieldRequired)
 .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
.MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 1000));


            RuleFor(m => m.PosterURL)
.NotEmpty().WithMessage(ValidationResources.FieldRequired);



            RuleFor(m => m.Rate)
    .Must(value => Enum.IsDefined(typeof(RatingEnum), value))
    .WithMessage(SystemResources.InvalidRating);


            RuleFor(m => m.DurationInMinutes)
                .GreaterThan(0).WithMessage(_ => string.Format(ValidationResources.GreaterThan, 0));

            RuleFor(m => m.ReleaseYear)
                .GreaterThan(1800).WithMessage(_ => string.Format(ValidationResources.GreaterThan, 1800));
        }
        private void ApplyCustomRules()
        {
            RuleFor(m => m.Id)
                .MustAsync(async (key, CancellationToken) => await _movieService.IsExistAsync(key))
                .WithMessage(SystemResources.NotExist);

            RuleFor(a => a.Poster)
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
     });




            RuleFor(m => m.TitleEn).MustAsync(async (Models, key, CancellationToken) =>
            {
                return !await _movieService.IsExistByNameExcludeItselfAsync(Models.Id, key, Models.TitleAr);
            }).WithMessage(SystemResources.NameAlreadyExists);

            //Check If Genre Is not Exist 
            RuleForEach(m => m.GenresIds)
                .MustAsync(async (key, CancellationToken) => await _genreService.IsExistAsync(key))
                .WithMessage(SystemResources.NotExist);

            //Check If Actor Is not Exist 
            RuleForEach(m => m.ActorsIds)
                .MustAsync(async (key, CancellationToken) => await _actorService.IsExistAsync(key))
                .WithMessage(SystemResources.NotExist);


            //Check if SeatType is Not Exist
            RuleFor(s => s.DirectorId).MustAsync(async (key, CancellationToken) =>
            {
                return await _directorService.IsExistAsync(key);
            }).WithMessage(SystemResources.NotExist);

        }
    }
}
