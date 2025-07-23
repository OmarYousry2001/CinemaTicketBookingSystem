using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Models;
using CinemaTicketBookingSystem.Core.Filters;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.DataEntry)]
    public class DirectorsController : AppControllerBase
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet(Router.DirectorRouting.list)]
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
        [HttpPut(Router.DirectorRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDirector([FromForm] AddDirectorCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpPut(Router.DirectorRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditDirector([FromForm] EditDirectorCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [ServiceFilter(typeof(DataEntryRoleFilter))]
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
