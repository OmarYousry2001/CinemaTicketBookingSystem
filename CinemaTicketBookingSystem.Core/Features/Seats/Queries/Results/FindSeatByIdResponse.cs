

using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results.Shared;
namespace CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results

{
    public class FindSeatByIdResponse
    {
        public Guid Id { get; set; }
        public string SeatNumber { get; set; } = default!;
        public HallInSeatResponse Hall { get; set; }
        public SeatTypeInSeatResponse SeatType { get; set; }
    }
}
