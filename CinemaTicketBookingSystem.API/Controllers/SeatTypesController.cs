using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.DataEntry)]
    public class SeatTypesController : AppControllerBase
    {
        #region Queries Actions

        /// <summary>
        /// Get a list of all seat types.
        /// </summary>
        /// <returns>List of seat types.</returns>
        [HttpGet(Router.SeatTypeRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeatTypesAsync()
        {
            var response = await Mediator.Send(new GetAllSeatTypesQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get a specific seat type by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the seat type.</param>
        /// <returns>Seat type details if found.</returns>
        [HttpGet(Router.SeatTypeRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSeatTypeByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindSeatTypeByIdQuery() { Id = id });
            return NewResult(response);
        }

        #endregion

        #region Commands Actions

        /// <summary>
        /// Add a new seat type.
        /// </summary>
        /// <param name="model">The seat type data to add.</param>
        /// <returns>Result of the add operation.</returns>
        [HttpPost(Router.SeatTypeRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSeatType([FromBody] AddSeatTypeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Edit an existing seat type.
        /// </summary>
        /// <param name="model">The updated seat type data.</param>
        /// <returns>Result of the update operation.</returns>
        [HttpPut(Router.SeatTypeRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditSeatType([FromBody] EditSeatTypeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a seat type by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the seat type to delete.</param>
        /// <returns>Result of the delete operation.</returns>
        [HttpDelete(Router.SeatTypeRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeatType(Guid id)
        {
            var response = await Mediator.Send(new DeleteSeatTypeCommand() { Id = id });
            return NewResult(response);
        }

        #endregion
    }
}
