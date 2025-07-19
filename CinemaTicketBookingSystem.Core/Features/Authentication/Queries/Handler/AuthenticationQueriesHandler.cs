using CinemaTicketBookingSystem.Core.Features.Authentication.Queries.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Authentication.Queries.Handler
{
    public class AuthenticationQueriesHandler : ResponseHandler,
        IRequestHandler<GetAccessTokenValidityQuery, Response<string>>
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationUserService _userService;

        #endregion

        #region Constructors
        public AuthenticationQueriesHandler(IAuthenticationService authenticationService, IApplicationUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }
        #endregion

        public async Task<Response<string>> Handle(GetAccessTokenValidityQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ValidateAccessTokenAsync(request.AccessToken);
            if (result != UserResources.NotExpiredToken)
                return Unauthorized<string>(result);

            return Success(UserResources.NotExpiredToken);
        }

    }
}
