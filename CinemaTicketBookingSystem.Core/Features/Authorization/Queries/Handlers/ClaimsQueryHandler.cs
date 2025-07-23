using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
    {
        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly IApplicationUserService _userService;
        #endregion
        #region Constructors
        public ClaimsQueryHandler(
                                  IAuthorizationService authorizationService,
                                  IApplicationUserService userService) 
        {
            _authorizationService = authorizationService;
            _userService = userService;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.UserId);
            if (user == null) return NotFound<ManageUserClaimsResponse>(ValidationResources.UserNotFound);
            var userClaim = await _authorizationService.ManageUserClaimData(user);
            var mappedUserClaim = new ManageUserClaimsResponse
            {
                UserId = userClaim.UserId,
                userClaims = userClaim.userClaims.Select(c => new UserClaimsInManageUserClaimsResponse
                {
                    Type = c.Type,
                    Value = c.Value,
                }).ToList()
            };
            return Success(mappedUserClaim);
        }
        #endregion
    }
}
