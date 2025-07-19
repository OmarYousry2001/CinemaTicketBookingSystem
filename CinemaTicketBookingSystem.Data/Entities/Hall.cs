using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Hall : BaseEntity
    {
        public string NameAr { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public int Capacity { get; set; }
        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public ICollection<ShowTime> ShowTimes { get; set; } = new HashSet<ShowTime>();
    }
}
