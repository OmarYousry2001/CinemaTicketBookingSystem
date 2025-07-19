using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.User
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            AddUserMapping();
            //GetUserPaginationMapping();
            FindUserByIdMapping();
            EditUserMapping();
        }
    }
}
