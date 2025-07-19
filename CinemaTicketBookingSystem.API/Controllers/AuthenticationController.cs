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
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

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
