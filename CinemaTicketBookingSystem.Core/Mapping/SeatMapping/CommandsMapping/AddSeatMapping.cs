

using CinemaTicketBookingSystem.Core.Features.Seats.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Core.Mapping.SeatMapping
{
    public partial class SeatProfile
    {
        public void AddSeatMapping()
        {
            CreateMap<AddSeatCommand, Seat>();
        }
    }
}
