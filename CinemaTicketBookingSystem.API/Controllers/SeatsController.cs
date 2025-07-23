using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    [Authorize]
    public class SeatsController : AppControllerBase
    {


        #region Queries Actions

        [HttpGet(Router.SeatRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeatsAsync()
        {
            var response = await Mediator.Send(new GetAllSeatsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.SeatRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSeatByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindSeatByIdQuery() { Id = id });
            return NewResult(response);
        }
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
        [HttpPost(Router.SeatRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSeat([FromBody] AddSeatCommand model)
        {
        var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpPut(Router.SeatRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSeat([FromBody] EditSeatCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }
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
