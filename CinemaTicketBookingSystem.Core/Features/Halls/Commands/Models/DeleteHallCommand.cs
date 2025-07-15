using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models
{
    public class DeleteHallCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
