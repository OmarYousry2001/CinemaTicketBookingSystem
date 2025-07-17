using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class MovieActor : BaseEntity
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
        public virtual Movie Movie { get; set; } 
        public virtual Actor Actor { get; set; } 
    }

}
