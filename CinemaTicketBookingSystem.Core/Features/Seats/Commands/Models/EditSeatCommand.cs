using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;
using System;

namespace CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models
{
    public class EditSeatCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; } = default!;
        public string SeatNumber { get; set; } = default!;
        public Guid HallId { get; set; }
        public Guid SeatTypeId { get; set; }
        public EditSeatCommand() { }

        public EditSeatCommand(Guid id, string seatNumber, Guid hallId, Guid seatTypeId)
        {
            SeatNumber = seatNumber.Trim();
            HallId = hallId;
            SeatTypeId = seatTypeId;
        }
    }
}
