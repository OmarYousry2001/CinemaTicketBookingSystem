

namespace CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results
{
    public class GetAllActorsResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
    }

}
