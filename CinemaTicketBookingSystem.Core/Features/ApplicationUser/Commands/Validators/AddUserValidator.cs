using CinemaTicketBookingSystem.Core.Features.Users.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Users.Commands.Validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields

        private readonly IApplicationUserService _userService;

        #endregion

        #region Constructor

        public AddUserValidator(IApplicationUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        #endregion

        #region Private Methods

        private void ApplyValidationRules()
        {
            RuleFor(u => u.FullName)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));

            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
                .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage(ValidationResources.EmailFormat);

            RuleFor(u => u.PhoneNumber)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .Matches(@"^(?:(?:\+?20|0020|0)?1[0125]\d{8}|(?:\+?966|00966|0)?5\d{8})$")
                .WithMessage(ValidationResources.InvalidPhoneNumber);

            RuleFor(u => u.ConfirmPassword)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                .NotNull().WithMessage(ValidationResources.FieldRequired)
                .Equal(u => u.Password)
                .WithMessage(ValidationResources.InvalidConfirmPassword);
        }

        private void ApplyCustomValidationRules()
        {
            // Ensure that the provided email does not already exist in the system
            RuleFor(u => u.Email)
                .MustAsync(async (email, cancellationToken) =>
                {
                    return await _userService.FindByEmailAsync(email) is null;
                })
                .WithMessage(SystemResources.AlreadyExists);
        }

        #endregion
    }
}
