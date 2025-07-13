


using CinemaTicketBookingSystem.Data.Base;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Enums;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Reservation : BaseEntity
    {
        public decimal FinalPrice { get; set; }

        //payment tracking
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.Pending;

        // Audit fields

        public Guid ShowTimeId { get; set; }
        public string UserId { get; set; } = default!;
        public virtual ShowTime ShowTime { get; set; } = new();
        public virtual ICollection<ReservationSeat> ReservationSeats { get; set; } = new HashSet<ReservationSeat>();
        public virtual ApplicationUser User { get; set; } 


        public DateTime AllowedTime { get; set; } = DateTime.Now.AddMinutes(15);
    }
}
