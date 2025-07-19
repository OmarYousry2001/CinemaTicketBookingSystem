using CinemaTicketBookingSystem.Core.Features.Authentication.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields
        #endregion

        #region Constructors
        public SignInValidator()
        {
            ApplyValidationRules();
        }
        #endregion
        private void ApplyValidationRules()
        {
            RuleFor(s => s.Email)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                 .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                     .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage(ValidationResources.EmailFormat);


            RuleFor(s => s.Password)
              .NotNull().WithMessage(ValidationResources.FieldRequired)
                 .NotEmpty().WithMessage(ValidationResources.FieldRequired);
        }
    }
}
