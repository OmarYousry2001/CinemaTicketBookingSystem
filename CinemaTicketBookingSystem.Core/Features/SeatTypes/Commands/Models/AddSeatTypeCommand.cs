using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;
using System.Reflection.Metadata;

namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Commands.Models
{
    public class AddSeatTypeCommand : IRequest<Response<string>>
    {
        public string TypeNameAr { get; set; } = default!;
        public string TypeNameEn { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }

        public AddSeatTypeCommand(string typeNameAr, string typeNameEn,  decimal seatTypePrice)
        {
            TypeNameAr = typeNameAr.Trim();
            TypeNameEn = typeNameEn.Trim();     
            SeatTypePrice = seatTypePrice;
        }
    }
}
