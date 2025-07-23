using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Models;
using CinemaTicketBookingSystem.Core.Filters;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.DataEntry)]
    public class DirectorsController : AppControllerBase
    {
        #region Queries Actions
        /// <summary>
        /// Get a list of all directors.
        /// </summary>
        /// <returns>Returns a list of all directors.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Router.DirectorRouting.list)]
        public async Task<IActionResult> GetAllDirectorsAsync()
        {
            var response = await Mediator.Send(new GetAllDirectorsQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get a director by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the director.</param>
        /// <returns>Returns the director details if found.</returns>
        [HttpGet(Router.DirectorRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDirectorByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindDirectorByIdQuery() { Id = id });
            return NewResult(response);
        } 
        #endregion

        #region Commands Actions

        /// <summary>
        /// Create a new director.
        /// </summary>
        /// <param name="model">The data for the new director.</param>
        /// <returns>Returns the created director.</returns>
        [HttpPut(Router.DirectorRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDirector([FromForm] AddDirectorCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Edit an existing director.
        /// </summary>
        /// <param name="model">The updated director data.</param>
        /// <returns>Returns the updated director.</returns>
        [HttpPut(Router.DirectorRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditDirector([FromForm] EditDirectorCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a director by ID.
        /// </summary>
        /// <param name="id">The ID of the director to delete.</param>
        /// <returns>Returns the result of the delete operation.</returns>
        [ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpDelete(Router.DirectorRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteDirector(Guid id)
        {
            var response = await Mediator.Send(new DeleteDirectorCommand() { Id = id });
            return NewResult(response);
        }

        #endregion
    }
}
