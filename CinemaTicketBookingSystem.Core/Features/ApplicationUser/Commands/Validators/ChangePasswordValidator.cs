using CinemaTicketBookingSystem.Core.Features.Users.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace MovieReservationSystem.Core.Features.Users.Commands.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        #region Fields
        private readonly IApplicationUserService _userService;
        #endregion

        #region Constructors
        public ChangePasswordValidator(IApplicationUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

        private void ApplyValidationRules()
        {
            RuleFor(u => u.ConfirmPassword)
           .NotEmpty().WithMessage(ValidationResources.FieldRequired)
           .NotNull().WithMessage(ValidationResources.FieldRequired)
           .Equal(u => u.Password).WithMessage(ValidationResources.InvalidConfirmPassword);
        }
        private void ApplyCustomValidationRules()
        {
            //Check if user is Exist by Id
            RuleFor(u => u.Id).MustAsync(async (key, CancellationToken) =>
            {
                return await _userService.FindByIdAsync(key) is not null;
            }).WithMessage(ValidationResources.EntityNotFound);
        }
    }
}
