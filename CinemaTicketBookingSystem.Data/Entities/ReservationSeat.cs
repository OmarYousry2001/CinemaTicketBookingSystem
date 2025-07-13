namespace CinemaTicketBookingSystem.Data.Entities
{
    public class ReservationSeat
    {
        public Guid ReservationId { get; set; }
        public Guid SeatId { get; set; }

        public virtual Reservation Reservation { get; set; } = new();
        public virtual Seat Seat { get; set; } = new();
    }
}
