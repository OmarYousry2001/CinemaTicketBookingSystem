using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class MovieGenre
    {
        public Guid MovieId { get; set; }
        public Guid GenreId { get; set; }
        public virtual Movie Movie { get; set; } = new();
        public virtual Genre Genre { get; set; } = new();
    }

}
