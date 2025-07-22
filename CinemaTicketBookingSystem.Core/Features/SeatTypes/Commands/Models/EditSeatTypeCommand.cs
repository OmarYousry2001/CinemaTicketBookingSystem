using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models
{
    public class EditSeatTypeCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string TypeNameAr { get; set; } = default!;
        public string TypeNameEn { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }

        public EditSeatTypeCommand(Guid id, string typeNameAr, string typeNameEn, decimal seatTypePrice)
        {
            Id = id;
            TypeNameAr = typeNameAr.Trim();
            TypeNameEn = typeNameEn.Trim();
            SeatTypePrice = seatTypePrice;
        }
    }
}
