

namespace CinemaTicketBookingSystem.Core.Features.Directors.Queries.Results
{
    public class FindDirectorByIdResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
    }
}
