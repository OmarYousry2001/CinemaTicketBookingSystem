using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketBookingSystem.API.Controllers
{
    [Authorize(Roles = Roles.DataEntry)]
    [ApiController]
    public class GenresController : AppControllerBase
    {
        #region Queries Actions

        /// <summary>
        /// Get a list of all genres.
        /// </summary>
        /// <returns>Returns a list of all genres.</returns>
        [HttpGet(Router.GenreRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            var response = await Mediator.Send(new GetAllGenresQuery());
            return NewResult(response);
        }

        /// <summary>
        /// Get a genre by its ID.
        /// </summary>
        /// <param name="id">The ID of the genre.</param>
        /// <returns>Returns the genre details if found.</returns>
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

        /// <summary>
        /// Create a new genre.
        /// </summary>
        /// <param name="command">The genre data to create.</param>
        /// <returns>Returns the created genre.</returns>
        [HttpPost(Router.GenreRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGenre([FromBody] AddGenreCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        /// <summary>
        /// Edit an existing genre.
        /// </summary>
        /// <param name="model">The updated genre data.</param>
        /// <returns>Returns the updated genre.</returns>
        [HttpPut(Router.GenreRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditGenre([FromBody] EditGenreCommand model)
        {
            var response = await Mediator.Send(model);
            return NewResult(response);
        }

        /// <summary>
        /// Delete a genre by ID.
        /// </summary>
        /// <param name="id">The ID of the genre to delete.</param>
        /// <returns>Returns the result of the delete operation.</returns>
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
