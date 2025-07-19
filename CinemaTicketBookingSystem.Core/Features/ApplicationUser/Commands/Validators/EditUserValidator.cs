using CinemaTicketBookingSystem.Core.Features.Users.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;
namespace CinemaTicketBookingSystem.Core.Features.Users.Commands.Validators
{
    public class EditUserValidator : AbstractValidator<EditUserCommand>
    {
        #region Fields
        private readonly IApplicationUserService _userService;
        #endregion

        #region Constructors
        public EditUserValidator(IApplicationUserService userService)
        {
            _userService = userService;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        #endregion

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
            .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage(ValidationResources.EmailFormat);

            RuleFor(u => u.PhoneNumber)
         .NotEmpty().WithMessage(ValidationResources.FieldRequired)
          .NotNull().WithMessage(ValidationResources.FieldRequired)
            .Matches(@"^(?:(?:\+?20|0020|0)?1[0125]\d{8}|(?:\+?966|00966|0)?5\d{8})$")
            .WithMessage(ValidationResources.InvalidPhoneNumber);

        }
        private void ApplyCustomValidationRules()
        {
            RuleFor(u => u.Id).MustAsync(async (key, CancellationToken) =>
            {
                return await _userService.FindByIdAsync(key) is not null;
            }).WithMessage(ValidationResources.EntityNotFound);

            RuleFor(u => u.Email).MustAsync(async (model, key, CancellationToken) =>
            {
                var user = await _userService.FindByEmailAsync(key);
                return !(user is not null && model.Id != user.Id);
            }).WithMessage(SystemResources.AlreadyExists);

            RuleFor(u => u.Password).MustAsync(async (model, key, CancellationToken) =>
            {
                var user = await _userService.FindByIdAsync(model.Id);
                return await _userService.CheckPasswordAsync(user, key);
            }).WithMessage(ValidationResources.UserNotFound);
        }
    }
}
