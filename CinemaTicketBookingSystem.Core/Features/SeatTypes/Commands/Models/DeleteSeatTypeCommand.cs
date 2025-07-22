using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models
{
    public class DeleteSeatTypeCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
