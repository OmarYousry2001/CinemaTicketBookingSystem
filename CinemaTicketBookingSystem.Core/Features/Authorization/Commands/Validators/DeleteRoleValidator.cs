using CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using FluentValidation;

namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        public readonly IAuthorizationService _authorizationService;

        #endregion
        #region Constructors
        public DeleteRoleValidator(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region  Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(ValidationResources.FieldRequired)
                 .NotNull().WithMessage(ValidationResources.FieldRequired);
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
