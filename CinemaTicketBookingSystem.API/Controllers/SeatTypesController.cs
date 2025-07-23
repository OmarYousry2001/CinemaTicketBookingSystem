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
        [HttpGet(Router.SeatTypeRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSeatTypesAsync()
        {
            var response = await Mediator.Send(new GetAllSeatTypesQuery());
            return NewResult(response);
        }

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
        [HttpPost(Router.SeatTypeRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSeatType([FromBody] AddSeatTypeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpPut(Router.SeatTypeRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditSeatType([FromBody] EditSeatTypeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

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
