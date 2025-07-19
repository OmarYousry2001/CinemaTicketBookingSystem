using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models
{
    public class DeleteSeatCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
