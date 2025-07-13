using CinemaTicketBookingSystem.Data.Base;

namespace CinemaTicketBookingSystem.Data.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string ImageURL { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
        public Actor? Actor { get; set; }
        public Director? Director { get; set; }
    }
}
