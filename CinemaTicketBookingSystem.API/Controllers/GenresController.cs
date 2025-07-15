using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CinemaTicketBookingSystem.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class GenresController : AppControllerBase
    {


        #region Queries Actions
        //[Authorize(Roles = "Data Entry")]
        [HttpGet(Router.GenreRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGenresAsync()
        {
             var response = await Mediator.Send(new GetAllGenresQuery());
            return NewResult(response);
        }

        [HttpGet(Router.GenreRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGenreByIdAsync(Guid id)
        {
             var response = await Mediator.Send(new FindGenreByIdQuery() { Id = id });
            return NewResult(response);
        }
        #endregion

        #region Commands Actions
        //[ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPost(Router.GenreRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGenre([FromBody] AddGenreCommand command)
        {
             var response = await Mediator.Send(command);
            return NewResult(response);
        }

        //[ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpPut(Router.GenreRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditGenre([FromBody] EditGenreCommand model)
        {
             var response = await Mediator.Send(model);
            return NewResult(response);
        }

        //[ServiceFilter(typeof(DataEntryRoleFilter))]
        [HttpDelete(Router.GenreRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteGenre(Guid id)
        {
             var response = await Mediator.Send(new DeleteGenreCommand() { Id = id });
            return NewResult(response);
        }
        #endregion
    }
}
