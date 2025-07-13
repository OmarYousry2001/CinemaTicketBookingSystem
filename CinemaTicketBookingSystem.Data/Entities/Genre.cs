using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Genre : BaseEntity 
    {
        public string NameAr { get; set; } = default!;
        public string NameEn { get; set; } = default!;

        public virtual ICollection<MovieGenre>? MovieGenres { get; set; }
    }
}
