using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CinemaTicketBookingSystem.API.Controllers
{
    public class ShowTimesController : AppControllerBase
    {


        #region Queries Actions
        [Authorize(Roles = "Data Entry")]
        [HttpGet(Router.ShowTimeRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShowTimesAsync()
        {
           var response = await Mediator.Send(new GetAllShowTimesQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ShowTimeRouting.comingList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComingShowTimesAsync()
        {
           var response = await Mediator.Send(new GetComingShowTimesQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ShowTimeRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShowTimeByIdAsync(Guid id)
        {
           var response = await Mediator.Send(new FindShowTimeByIdQuery() { Id = id });
            return NewResult(response);
        }
        #endregion

        #region Commands Actions
        [Authorize(Roles = "Data Entry")]
        [HttpPost(Router.ShowTimeRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShowTime([FromBody] AddShowTimeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [Authorize(Roles = "Data Entry")]
        [HttpPut(Router.ShowTimeRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditShowTime([FromBody] EditShowTimeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [Authorize(Roles = "Data Entry")]
        [HttpDelete(Router.ShowTimeRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteShowTime(Guid id)
        {
            var response = await Mediator.Send(new DeleteShowTimeCommand() { Id = id });
            return NewResult(response);
        }
        #endregion
    }
}
