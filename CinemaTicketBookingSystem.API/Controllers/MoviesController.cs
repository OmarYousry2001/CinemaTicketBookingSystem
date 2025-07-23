using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Movies.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.DataEntry)]
    public class MoviesController : AppControllerBase
    {


        #region Queries Actions
        [HttpGet(Router.MovieRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllMoviesAsync()
        {
            var response = await Mediator.Send(new GetAllMoviesQuery());
            return NewResult(response);
        }

        [AllowAnonymous]
        [HttpGet(Router.MovieRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMovieByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindMovieByIdQuery() { Id = id });
            return NewResult(response);
        }

        [AllowAnonymous]
        [HttpGet(Router.MovieRouting.PaginatedList)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMoviesPaginatedList([FromQuery] GetMoviesPaginatedListQuery model)
        {
            var response = await Mediator.Send(model);
            return Ok(response);
        }
        #endregion

        #region Commands Actions
        [HttpPost(Router.MovieRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMovie([FromForm] AddMovieCommand model)
        {
           var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpPut(Router.MovieRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditMovie([FromForm] EditMovieCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        [HttpDelete(Router.MovieRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var response = await Mediator.Send(new DeleteMovieCommand() { Id = id });
            return NewResult(response);
        }
        #endregion
    }
}
