
using CinemaTicketBookingSystem.Core.Features.Users.Commands.Models;
using CinemaTicketBookingSystem.Data.Entities.Identity;

namespace CinemaTicketBookingSystem.Core.Mapping.User
{
    public partial class ApplicationUserProfile
    {
        public void EditUserMapping()
        {
            CreateMap<EditUserCommand, ApplicationUser>();
        }
    }
}
