using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.DataEntry)]
    public class SeatsController : AppControllerBase
    {
        #region Queries Actions

        /// <summary>
        /// Get a list of all seats.
        /// </summary>
        /// <returns>List of all seats.</returns>
        [HttpGet(Router.SeatRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeatsAsync()
        {
            var response = await Mediator.Send(new GetAllSeatsQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get details of a specific seat by its ID.
        /// </summary>
        /// <param name="id">ID of the seat.</param>
        /// <returns>Seat details if found.</returns>
        [HttpGet(Router.SeatRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSeatByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindSeatByIdQuery() { Id = id });
            return NewResult(response);
        }

        /// <summary>
        /// Get a list of free seats in a specific show time.
        /// </summary>
        /// <param name="showTimeId">ID of the show time.</param>
        /// <returns>List of available seats.</returns>
        [AllowAnonymous]
        [HttpGet(Router.SeatRouting.FreeSeatsInShowTime)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreeSeatsInShowTimeAsync([FromRoute] Guid showTimeId)
        {
            var response = await Mediator.Send(new GetFreeSeatsInShowTimeQuery() { ShowTimeId = showTimeId });
            return NewResult(response);
        }

        #endregion

        #region Commands Actions

        /// <summary>
        /// Create a new seat.
        /// </summary>
        /// <param name="model">Seat data to be added.</param>
        /// <returns>Status of creation.</returns>
        [HttpPost(Router.SeatRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeat([FromBody] AddSeatCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Update an existing seat.
        /// </summary>
        /// <param name="model">Updated seat data.</param>
        /// <returns>Status of update.</returns>
        [HttpPut(Router.SeatRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSeat([FromBody] EditSeatCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a seat by ID.
        /// </summary>
        /// <param name="id">ID of the seat to delete.</param>
        /// <returns>Status of deletion.</returns>
        [HttpDelete(Router.SeatRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeat(Guid id)
        {
            var response = await Mediator.Send(new DeleteSeatCommand() { Id = id });
            return NewResult(response);
        }

        #endregion
    }
}
