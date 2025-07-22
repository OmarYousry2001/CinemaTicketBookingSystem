
using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results.Shared;

namespace CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results
{
    public class FindReservationByIdResponse
    {
        public Guid Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal FinalPrice { get; set; }
        public string HallName { get; set; } = default!;
        public ShowTimeInReservationResponse ShowTime { get; set; } = default!;
        public ICollection<SeatsInReservationResponse> Seats { get; set; } = default!;
        public UserInReservationResponse User { get; set; } = default!;
    }
}
