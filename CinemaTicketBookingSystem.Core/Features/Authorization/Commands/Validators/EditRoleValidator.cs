using CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        public readonly IAuthorizationService _authorizationService;
        #endregion
        #region 
        public EditRoleValidator(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }       

        #endregion
        public EditRoleValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                 .NotNull().WithMessage(ValidationResources.FieldRequired);

            RuleFor(x => x.Name)
             .NotEmpty().WithMessage(ValidationResources.FieldRequired)
              .MinimumLength(2).WithMessage(_ => string.Format(ValidationResources.MinimumLength, 2))
             .MaximumLength(100).WithMessage(_ => string.Format(ValidationResources.MaxLengthExceeded, 100));
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Id)
          .MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistByIdAsync(Key))
          .WithMessage(SystemResources.NotExist);
        }

        #endregion
    }
}
