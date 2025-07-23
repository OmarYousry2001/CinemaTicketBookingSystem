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

        /// <summary>
        /// Get a list of all users (Admin only).
        /// </summary>
        /// <returns>List of all users.</returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet(Router.UserRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await Mediator.Send(new GetAllUsersQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get user details by user ID (Admin only).
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>User details.</returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet(Router.UserRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserByIdAsync(string id)
        {
            var response = await Mediator.Send(new FindUserByIdQuery() { Id = id });
            return NewResult(response);
        }

        /// <summary>
        /// Get reservation history for a user.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>List of reservations.</returns>
        [Authorize(Roles = $"{Roles.DataEntry},{Roles.User}")]
        [HttpGet(Router.UserRouting.UserReservations)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserReservationsAsync(string id)
        {
            var response = await Mediator.Send(new GetUserReservationsHistoryQuery() { Id = id });
            return NewResult(response);
        }

        #endregion

        #region Commands Actions

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="model">The user creation model.</param>
        /// <returns>Created user result.</returns>
        [HttpPost(Router.UserRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Edit user information.
        /// </summary>
        /// <param name="model">The user edit model.</param>
        /// <returns>Updated user result.</returns>
        [Authorize]
        [HttpPut(Router.UserRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Change the user's password.
        /// </summary>
        /// <param name="model">The change password model.</param>
        /// <returns>Change password result.</returns>
        [Authorize]
        [HttpPut(Router.UserRouting.ChangePassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a user by ID (Data Entry only).
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Deletion result.</returns>
        [Authorize(Roles = Roles.DataEntry)]
        [HttpDelete(Router.UserRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await Mediator.Send(new DeleteUserCommand() { Id = id });
            return NewResult(response);
        }

        /// <summary>
        /// Confirm user's email using a code.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="code">Confirmation code.</param>
        /// <returns>Confirmation result.</returns>
        [HttpGet(Router.UserRouting.ConfirmEmail)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var response = await Mediator.Send(new ConfirmEmailQuery() { UserId = userId, Code = code });
            return NewResult(response);
        }

        /// <summary>
        /// Send a password reset email.
        /// </summary>
        /// <param name="command">Request to send reset email.</param>
        /// <returns>Result of sending reset email.</returns>
        [HttpPost(Router.UserRouting.SendResetPassword)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendResetPassword([FromForm] SendResetPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// Confirm the reset password code sent to email.
        /// </summary>
        /// <param name="command">Request containing reset code.</param>
        /// <returns>Result of confirmation.</returns>
        [HttpPost(Router.UserRouting.ConfirmResetPasswordCode)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConfirmResetPasswordCode([FromForm] ConfirmResetPasswordCodeCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// Reset the user's password using confirmation code.
        /// </summary>
        /// <param name="command">Password reset model.</param>
        /// <returns>Password reset result.</returns>
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
