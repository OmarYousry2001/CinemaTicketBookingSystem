using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    public class DirectorsController : AppControllerBase
    {


        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDirectorsAsync()
        {
            var response = await Mediator.Send(new GetAllDirectorsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.DirectorRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDirectorByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindDirectorByIdQuery() { Id = id });
            return NewResult(response);
        }


        #region Commands Actions
        [Authorize(Roles = "Data Entry")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDirector([FromForm] AddDirectorCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [Authorize(Roles = "Data Entry")]
        [HttpPut(Router.DirectorRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditDirector([FromForm] EditDirectorCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [Authorize(Roles = "Data Entry")]
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
