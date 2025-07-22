using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Reservations.Commands.Models
{
    public class AddReservationCommand : IRequest<Response<string>>
    {
        public Guid ShowTimeId { get; set; }
        public string UserId { get; set; } = default!;
        public List<Guid> SeatIds { get; set; }
    }
}
