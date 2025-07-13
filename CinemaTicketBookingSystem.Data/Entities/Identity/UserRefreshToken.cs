using CinemaTicketBookingSystem.Data.Base;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketBookingSystem.Data.Entities.Identity
{
    public class UserRefreshToken : BaseEntity
    {

        public string UserId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpiryDate { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? user { get; set; }
    }
}
