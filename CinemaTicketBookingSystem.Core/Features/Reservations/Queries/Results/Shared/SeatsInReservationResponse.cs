namespace CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results.Shared
{
    public class SeatsInReservationResponse
    {
        public Guid Id { get; set; }
        public string SeatNumber { get; set; } = default!;
    }
}
