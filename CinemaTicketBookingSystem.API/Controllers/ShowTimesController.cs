using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [Authorize(Roles = Roles.DataEntry)]
    [ApiController]
    public class ShowTimesController : AppControllerBase
    {
        #region Queries Actions

        /// <summary>
        /// Get a list of all showtimes.
        /// </summary>
        /// <returns>Returns a list of all scheduled showtimes.</returns>
        [HttpGet(Router.ShowTimeRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShowTimesAsync()
        {
            var response = await Mediator.Send(new GetAllShowTimesQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get a list of upcoming showtimes only.
        /// </summary>
        /// <returns>Returns a list of showtimes scheduled in the future.</returns>
        [HttpGet(Router.ShowTimeRouting.comingList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetComingShowTimesAsync()
        {
            var response = await Mediator.Send(new GetComingShowTimesQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get a showtime by its ID.
        /// </summary>
        /// <param name="id">The ID of the showtime.</param>
        /// <returns>Returns the showtime details if found.</returns>
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

        /// <summary>
        /// Create a new showtime.
        /// </summary>
        /// <param name="model">The showtime details to be created.</param>
        /// <returns>Returns the created showtime.</returns>
        [HttpPost(Router.ShowTimeRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShowTime([FromBody] AddShowTimeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Edit an existing showtime.
        /// </summary>
        /// <param name="model">The updated showtime details.</param>
        /// <returns>Returns the updated showtime.</returns>
        [HttpPut(Router.ShowTimeRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditShowTime([FromBody] EditShowTimeCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a showtime by ID.
        /// </summary>
        /// <param name="id">The ID of the showtime to delete.</param>
        /// <returns>Returns the result of the delete operation.</returns>
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
