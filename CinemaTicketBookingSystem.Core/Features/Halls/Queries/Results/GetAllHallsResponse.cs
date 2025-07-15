namespace CinemaTicketBookingSystem.Core.Features.Halls.Queries.Results
{
    public class GetAllHallsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
    }
}
