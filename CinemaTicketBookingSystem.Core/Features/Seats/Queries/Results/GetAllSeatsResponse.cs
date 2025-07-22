

using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results.Shared;
namespace CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results

{
    public class GetAllSeatsResponse
    {
        public Guid Id { get; set; }
        public string SeatNumber { get; set; } = default!;
        public HallInSeatResponse Hall { get; set; } = new();
        public SeatTypeInSeatResponse SeatType { get; set; } = new();
    }

}
