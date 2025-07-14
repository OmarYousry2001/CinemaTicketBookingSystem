using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class ShowTime :  BaseEntity
    {
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal ShowTimePrice { get; set; }
        public Guid HallId { get; set; }
        public Guid MovieId { get; set; }
        public virtual Hall Hall { get; set; } 
        public virtual Movie Movie { get; set; } 
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
