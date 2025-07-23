using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Users.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Users.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CinemaTicketBookingSystem.API.Controllers
{

    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {

        #region Queries Actions
        [HttpGet(Router.UserRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await Mediator.Send(new GetAllUsersQuery());
            return NewResult(response);
        }
        [HttpGet(Router.UserRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var response = await Mediator.Send(new FindUserByIdQuery() { Id = id });
            return NewResult(response);
        } 
        #endregion

        #region Commands Actions

        [HttpPost(Router.UserRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [Authorize]
        [HttpPut(Router.UserRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [Authorize]
        [HttpPut(Router.UserRouting.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete(Router.UserRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await Mediator.Send(new DeleteUserCommand() { Id = id });
            return NewResult(response);
        }

        [HttpGet(Router.UserRouting.ConfirmEmail)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var response = await Mediator.Send(new ConfirmEmailQuery() { UserId = userId, Code = code });
            return NewResult(response);
        }

        [HttpPost(Router.UserRouting.SendResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendResetPassword([FromForm] SendResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.UserRouting.ConfirmResetPasswordCode)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmResetPasswordCode([FromForm] ConfirmResetPasswordCodeCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost(Router.UserRouting.ResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        #endregion
    }
}
