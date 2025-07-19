
using CinemaTicketBookingSystem.Core.Features.Users.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities.Identity;

namespace CinemaTicketBookingSystem.Core.Mapping.User
{
    public partial class ApplicationUserProfile
    {
        public void FindUserByIdMapping()
        {
            CreateMap<ApplicationUser, FindUserByIdResponse>();
         
        }
    }
}
