namespace CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results.Shared
{
    public class ShowTimeInMovieResponse
    {
        public Guid Id { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string HallName { get; set; } = default!;        
    }
}
