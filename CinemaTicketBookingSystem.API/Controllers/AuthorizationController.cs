using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Quaries.Models;
using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CinemaTicketBookingSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {

        #region Queries Actions
        [HttpGet(Router.AuthorizationRouting.RoleList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRolesAsync()
        {
             var response = await Mediator.Send(new GetAllRolesQuery());
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRouting.GetRoleById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRoleByIdAsync([FromRoute] string id)
        {
             var response = await Mediator.Send(new FindRoleByIdQuery() { Id = id });
            return NewResult(response);
        }

        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        [SwaggerOperation(Summary = " Managing user Roles", OperationId = "ManageUserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] string userId)
        {
             var response = await Mediator.Send(new ManageUserRolesQuery() { UserId = userId });
            return NewResult(response);
        }

        [SwaggerOperation(Summary = " Manage User Claims", OperationId = "ManageUserClaims")]
        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] string userId)
        {
            var response = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }
        #endregion

        #region Commands Actions
        [HttpPost(Router.AuthorizationRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRole([FromForm] AddRoleCommand model)
        {
             var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpPut(Router.AuthorizationRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand model)
        {
             var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        [SwaggerOperation(Summary = " Update user Roles", OperationId = "ManageUserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand model)
        {
             var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.AuthorizationRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRole([FromRoute]  string id)
        {
             var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
   
        [SwaggerOperation(Summary = " Update User Claims", OperationId = "UpdateUserClaims")]
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        #endregion
    }
}
