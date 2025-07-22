namespace CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results
{
    public class GetFreeSeatsInShowTimeResponse
    {
        public Guid Id { get; set; }
        public string SeatNumber { get; set; } = default!;
        public string SeatTypeName { get; set; } = default!;
    }
}
