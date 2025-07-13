namespace CinemaTicketBookingSystem.Data.Entities
{
    public class MovieActor
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
        public virtual Movie Movie { get; set; } = new();
        public virtual Actor Actor { get; set; } = new();
    }

}
