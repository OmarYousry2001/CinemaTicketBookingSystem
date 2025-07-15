namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Results
{
    public class FindSeatTypeByIdResponse
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }
    }
}
