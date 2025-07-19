using CinemaTicketBookingSystem.Core.Features.Users.Queries.Models;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Identity;


namespace CinemaTicketBookingSystem.Core.Features.Users.Queries.Validator
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion

        #region Constructors
        public ConfirmEmailValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion


        private void ApplyValidationRules()
        {
            RuleFor(c => c.UserId)
        .NotNull().WithMessage(ValidationResources.FieldRequired)
       .NotEmpty().WithMessage(ValidationResources.FieldRequired);

            RuleFor(c => c.Code)
      .NotNull().WithMessage(ValidationResources.FieldRequired)
       .NotEmpty().WithMessage(ValidationResources.FieldRequired);
        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(c => c.UserId).MustAsync(async (key, CancellationToken) =>
            {
                return await _userManager.FindByIdAsync(key) is not null;
            }).WithMessage(SystemResources.Invalid);
        }
    }
}
