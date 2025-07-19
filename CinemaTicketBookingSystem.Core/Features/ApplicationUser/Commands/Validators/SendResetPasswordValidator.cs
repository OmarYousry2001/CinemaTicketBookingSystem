using CinemaTicketBookingSystem.Core.Features.Users.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Users.Commands.Validators
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        #region Fields

        private readonly IApplicationUserService _userService;

        #endregion

        #region Constructor

        public SendResetPasswordValidator(IApplicationUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        #endregion

        #region Private Methods

        private void ApplyValidationRules()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage(ValidationResources.EmailFormat);
        }

        private void ApplyCustomValidationRules()
        {
            RuleFor(r => r.Email)
                .MustAsync(async (email, cancellationToken) =>
                {
                    return await _userService.FindByEmailAsync(email) is not null;
                })
                .WithMessage(ValidationResources.EmailNotFound);
        }

        #endregion
    }
}
