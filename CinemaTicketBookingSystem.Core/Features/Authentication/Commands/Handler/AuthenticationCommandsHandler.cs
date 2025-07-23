using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Authentication.Commands.Models;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace CinemaTicketBookingSystem.Core.Features.Authentication.Commands.Handler
{
    public class AuthenticationCommandsHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<JwtAuthTokenResponse>>,
        IRequestHandler<RefreshTokenCommand, Response<JwtAuthTokenResponse>>

    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationUserService _userService;


        #endregion
        #region Constructors
        public AuthenticationCommandsHandler(IMapper mapper, IAuthenticationService authenticationService, IApplicationUserService userService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _userService = userService;
        }
        #endregion
        public async Task<Response<JwtAuthTokenResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user Exist by Username
            var user = await _userService.FindByEmailAsync(request.Email);
            if (user == null)
                return BadRequest<JwtAuthTokenResponse>(UserResources.Invalid_Email);

            //Check if Password is true for User
            var signInResult = await _userService.CheckPasswordAsync(user, request.Password);
            if (!signInResult)
                return BadRequest<JwtAuthTokenResponse>(UserResources.Password_Mismatch);

            if (!await _userService.IsEmailConfirmedAsync(user))
            {
                await _userService.SendConfirmUserEmailToken(user);
                return BadRequest<JwtAuthTokenResponse>(UserResources.User_EmailNotConfirmed);
            }
            //Generate JWTAuthToken
            var response = await _authenticationService.GetJwtTokenAsync(user);

            //return token
            return Success(response);
        }

        public async Task<Response<JwtAuthTokenResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //Read Access Token To Get Claims 
            var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
            var validationResult = await _authenticationService.ValidateBeforeRenewTokenAsync(jwtToken, request.AccessToken, request.RefreshToken);

            if (validationResult != null)
                return Unauthorized<JwtAuthTokenResponse>(validationResult);

            var userRefreshToken = await _authenticationService.GetUserFullRefreshTokenObjByRefreshToken(request.RefreshToken);
            var result = await _authenticationService.CreateNewAccessTokenByRefreshToken(request.AccessToken, userRefreshToken);
            return Success(result);
        }


    }
}
