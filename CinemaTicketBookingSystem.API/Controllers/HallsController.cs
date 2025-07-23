using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieReservationSystem.API.Controllers
{

    [ApiController]
    [Authorize(Roles = Roles.DataEntry)]
    public class HallsController : AppControllerBase
    {


        #region Queries Actions
        [HttpGet(Router.HallRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllHallsAsync()
        {
            var response = await Mediator.Send(new GetAllHallsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.HallRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetHallByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindHallByIdQuery() { Id = id });
            return NewResult(response);
        }
        #endregion

        #region Commands Actions
        [HttpPost(Router.HallRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateHall([FromBody] AddHallCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }


        [HttpPut(Router.HallRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditHall([FromBody] EditHallCommand model)
        {
             var response = await Mediator.Send(model);
            return NewResult(response);
        }


        [HttpDelete(Router.HallRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteHall(Guid id)
        {
             var response = await Mediator.Send(new DeleteHallCommand() { Id = id });
            return NewResult(response);
        }
        #endregion
    }
}
