using CinemaTicketBookingSystem.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Actor : BaseEntity
    {
   
        public Guid PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; } 
        public virtual ICollection<MovieActor> MovieActors { get; set; } = new HashSet<MovieActor>();

    }
}
