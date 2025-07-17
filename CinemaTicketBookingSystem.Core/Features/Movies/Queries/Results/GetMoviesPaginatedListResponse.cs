

using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results.Shared;
using CinemaTicketBookingSystem.Data.Enums;

namespace CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results
{
    public class GetMoviesPaginatedListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PosterURL { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public RatingEnum Rate { get; set; }
        public bool IsActive { get; set; }
        public DirectorInMovieResponse Director { get; set; } = new();
        public IEnumerable<GenreInMovieResponse> Genres { get; set; } = new List<GenreInMovieResponse>();
        public IEnumerable<ActorInMovieResponse> Actors { get; set; } = new List<ActorInMovieResponse>();
        public IEnumerable<ShowTimeInMovieResponse> ShowTimes { get; set; } = new List<ShowTimeInMovieResponse>();
    }
}
