using CinemaTicketBookingSystem.API.Base;
using CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models;
using CinemaTicketBookingSystem.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Models;
namespace CinemaTicketBookingSystem.API.Controllers
{
    [ApiController]
    public class ActorsController : AppControllerBase
    {
      
        [HttpGet(Router.ActorRouting.list)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllActorsAsync()
        {
            var response = await Mediator.Send(new GetAllActorsQuery());
            return NewResult(response);
        }

        [HttpGet(Router.ActorRouting.GetById)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActorByIdAsync(Guid id)
        {
            var response = await Mediator.Send(new FindActorsByIdQuery() { Id = id });
            return NewResult(response);
        }
        [HttpPost(Router.ActorRouting.Create)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateActor([FromForm] AddActorCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
           
        }

        [HttpPut(Router.ActorRouting.Edit)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditActor([FromForm] EditActorCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.ActorRouting.Delete)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteActor(Guid id)
        {
            var response = await Mediator.Send(new DeleteActorCommand() { Id = id });
            return NewResult(response);
        }
    
    }
}
