using CinemaTicketBookingSystem.Data.Base;
using CinemaTicketBookingSystem.Data.Enums;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Movie : BaseEntity
    {
        public string TitleAr { get; set; } = default!;
        public string TitleEn { get; set; } = default!;

        public string DescriptionAr { get; set; } = default!;
        public string DescriptionEn { get; set; } = default!;

        public string PosterURL { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public RatingEnum Rate { get; set; }
        public bool IsActive { get; set; }
        public Guid DirectorId { get; set; }
        public virtual Director Director { get; set; } 
        public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new HashSet<MovieGenre>();
        public virtual ICollection<ShowTime> ShowTimes { get; set; } = new HashSet<ShowTime>();
        public virtual ICollection<MovieActor> MovieActors { get; set; } = new HashSet<MovieActor>();

    }
}
