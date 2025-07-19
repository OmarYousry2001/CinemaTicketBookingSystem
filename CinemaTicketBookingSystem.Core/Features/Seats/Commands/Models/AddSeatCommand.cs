using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models
{
    public class AddSeatCommand : IRequest<Response<string>>
    {
        public string SeatNumber { get; set; } = default!;
        public Guid HallId { get; set; }
        public Guid SeatTypeId { get; set; }
        public AddSeatCommand() { }

        public AddSeatCommand(string seatNumber, Guid hallId, Guid seatTypeId)
        {
            SeatNumber = seatNumber.Trim();
            HallId = hallId;
            SeatTypeId = seatTypeId;
        }
    }
}
