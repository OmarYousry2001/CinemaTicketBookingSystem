using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Reservations.Commands.Models
{
    public class DeleteReservationCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
