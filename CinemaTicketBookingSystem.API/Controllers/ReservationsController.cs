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
        [HttpGet(Router.ReservationRouting.PaginatedList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservationsPaginatedList([FromQuery] GetReservationsPaginatedListQuery model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }
        [HttpGet(Router.ReservationRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReservationByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindReservationByIdQuery() { Id = id });
            return NewResult(response);
        }
        #endregion

        #region Commands Actions
        [Authorize(Roles = Roles.User)]
        [HttpPost(Router.ReservationRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReservation([FromBody] AddReservationCommand model)
        {
           var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.ReservationRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSeat(Guid id)
        {
           var response = await Mediator.Send(new DeleteReservationCommand() { Id = id });
            return NewResult(response);
        }
        #endregion
    }
}
