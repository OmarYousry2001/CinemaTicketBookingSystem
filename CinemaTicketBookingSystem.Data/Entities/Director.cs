using CinemaTicketBookingSystem.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Director : BaseEntity
    {
        public Guid PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; } 
        public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
