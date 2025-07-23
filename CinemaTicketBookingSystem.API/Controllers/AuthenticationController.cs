using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Authentication.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Authentication.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        #region Queries Actions

        /// <summary>
        /// Validates the current access token to check if it's still valid.
        /// </summary>
        /// <param name="model">The token validation request model.</param>
        /// <returns>Validation result indicating token status.</returns>
        [Authorize]
        [HttpPost(Router.AuthenticationRouting.ValidateToken)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ValidateToken([FromForm] GetAccessTokenValidityQuery model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        #endregion

        #region Commands Actions

        /// <summary>
        /// Signs in a user using their credentials.
        /// </summary>
        /// <param name="model">The sign-in request containing username and password.</param>
        /// <returns>Authentication result with tokens.</returns>
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Refreshes the access token using a valid refresh token.
        /// </summary>
        /// <param name="model">The refresh token request.</param>
        /// <returns>New access and refresh tokens.</returns>
        [HttpPost(Router.AuthenticationRouting.RefreshToken)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        #endregion
    }
}
