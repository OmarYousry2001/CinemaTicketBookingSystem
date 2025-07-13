using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Seat : BaseEntity
    {
        public string SeatNumber { get; set; } = default!;
        public Guid HallId { get; set; }
        public Guid SeatTypeId { get; set; }
        public Hall Hall { get; set; } 
        public SeatType SeatType { get; set; }
        public virtual ICollection<ReservationSeat> ReservationSeats { get; set; } = new HashSet<ReservationSeat>();
    }
}
