using CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : ResponseHandler,
         IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;

        #endregion
        #region Constructors
        public ClaimsCommandHandler( IAuthorizationService authorizationService) 
        {
            _authorizationService = authorizationService;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var userClaimsRequest = new UpdateUserClaimsRequest() 
            {
                UserId = request.UserId , 
                userClaims = request.userClaims.Select(c =>new UserClaims { Value = c.Value , Type = c.Type}).ToList() 
            };

            var result = await _authorizationService.UpdateUserClaims(userClaimsRequest);

            switch (result)
            {
                case nameof(ValidationResources.UserNotFound):
                    return NotFound<string>(ValidationResources.UserNotFound);

                case nameof(ValidationResources.FailedToRemoveOldClaims):
                    return BadRequest<string>(ValidationResources.FailedToRemoveOldClaims);

                case nameof(ValidationResources.FailedToAddNewClaims):
                    return BadRequest<string>(ValidationResources.FailedToAddNewClaims);

                case nameof(ValidationResources.FailedToUpdateClaims):
                    return BadRequest<string>(ValidationResources.FailedToUpdateClaims);
            }

            return Success<string>(NotifiAndAlertsResources.Success);

        }
        #endregion
    }
}
