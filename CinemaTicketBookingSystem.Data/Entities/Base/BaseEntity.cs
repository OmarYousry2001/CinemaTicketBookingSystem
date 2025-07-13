using CinemaTicketBookingSystem.Data.Commons;
using System.ComponentModel.DataAnnotations;

namespace CinemaTicketBookingSystem.Data.Base
{
    //Base class for entities common properties
    public class BaseEntity  : GeneralLocalizableEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int CurrentState { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedDateUtc { get; set; }
    }
}
