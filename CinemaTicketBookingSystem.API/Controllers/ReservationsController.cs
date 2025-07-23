using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Reservations.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    public class ReservationsController : AppControllerBase
    {
        #region Queries Actions

        /// <summary>
        /// Get a paginated list of reservations with optional filters.
        /// </summary>
        /// <param name="model">Pagination and filter parameters.</param>
        /// <returns>Paginated list of reservations.</returns>
        [HttpGet(Router.ReservationRouting.PaginatedList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = Roles.DataEntry)]
        public async Task<IActionResult> GetReservationsPaginatedList([FromQuery] GetReservationsPaginatedListQuery model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }

        /// <summary>
        /// Get reservation details by its unique ID.
        /// </summary>
        /// <param name="id">Reservation ID.</param>
        /// <returns>Reservation details if found; otherwise 404.</returns>
        [HttpGet(Router.ReservationRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = Roles.DataEntry)]
        public async Task<IActionResult> GetReservationByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindReservationByIdQuery() { Id = id });
            return NewResult(response);
        }

        #endregion

        #region Commands Actions

        /// <summary>
        /// Create a new reservation.
        /// </summary>
        /// <param name="model">Reservation data to create.</param>
        /// <returns>Result of the creation operation.</returns>
        [Authorize(Roles = Roles.User)]
        [HttpPost(Router.ReservationRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReservation([FromBody] AddReservationCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a reservation by ID.
        /// </summary>
        /// <param name="id">Reservation ID to delete.</param>
        /// <returns>Result of the deletion operation.</returns>
        [HttpDelete(Router.ReservationRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = Roles.DataEntry)]
        public async Task<IActionResult> DeleteSeat(Guid id)
        {
            var response = await Mediator.Send(new DeleteReservationCommand() { Id = id });
            return NewResult(response);
        }

        #endregion
    }
}
