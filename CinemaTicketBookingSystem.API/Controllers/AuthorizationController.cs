﻿using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Quaries.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class AuthorizationController : AppControllerBase
    {
        #region Queries Actions

        /// <summary>
        /// Get a list of all roles.
        /// </summary>
        /// <returns>List of roles.</returns>
        [HttpGet(Router.AuthorizationRouting.RoleList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var response = await Mediator.Send(new GetAllRolesQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get role details by ID.
        /// </summary>
        /// <param name="id">The ID of the role.</param>
        /// <returns>Role details.</returns>
        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] string id)
        {
            var response = await Mediator.Send(new FindRoleByIdQuery() { Id = id });
            return NewResult(response);
        }

        /// <summary>
        /// Get roles assigned to a user.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>User roles management data.</returns>
        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        [SwaggerOperation(Summary = "Managing user Roles", OperationId = "ManageUserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] string userId)
        {
            var response = await Mediator.Send(new ManageUserRolesQuery() { UserId = userId });
            return NewResult(response);
        }

        /// <summary>
        /// Get claims assigned to a user.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>User claims management data.</returns>
        [SwaggerOperation(Summary = "Manage User Claims", OperationId = "ManageUserClaims")]
        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] string userId)
        {
            var response = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }

        #endregion

        #region Commands Actions

        /// <summary>
        /// Create a new role.
        /// </summary>
        /// <param name="model">Role creation model.</param>
        /// <returns>Creation result.</returns>
        [HttpPost(Router.AuthorizationRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromForm] AddRoleCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Edit an existing role.
        /// </summary>
        /// <param name="model">Edit role model.</param>
        /// <returns>Edit result.</returns>
        [HttpPut(Router.AuthorizationRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Update roles assigned to a user.
        /// </summary>
        /// <param name="model">Update roles model.</param>
        /// <returns>Update result.</returns>
        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        [SwaggerOperation(Summary = "Update user Roles", OperationId = "ManageUserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a role by ID.
        /// </summary>
        /// <param name="id">Role ID to delete.</param>
        /// <returns>Deletion result.</returns>
        [HttpDelete(Router.AuthorizationRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }

        /// <summary>
        /// Update claims assigned to a user.
        /// </summary>
        /// <param name="command">Update claims model.</param>
        /// <returns>Update result.</returns>
        [SwaggerOperation(Summary = "Update User Claims", OperationId = "UpdateUserClaims")]
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        #endregion
    }
}
