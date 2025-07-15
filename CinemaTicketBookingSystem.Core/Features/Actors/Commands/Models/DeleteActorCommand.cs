using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models
{
    public class DeleteActorCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
