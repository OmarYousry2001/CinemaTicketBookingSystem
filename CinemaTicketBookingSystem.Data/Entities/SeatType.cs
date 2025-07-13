using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class SeatType : BaseEntity
    {
        public string TypeNameAr { get; set; } = default!;
        public string TypeNameEn { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }
        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
    }

}
