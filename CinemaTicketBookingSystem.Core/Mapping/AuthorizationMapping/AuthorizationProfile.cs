using AutoMapper;

namespace CinemaTicketBookingSystem.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            EditUserRoleMapping();
            FindRoleByIdMapping();
            GetAllRolesMapping();
            ManageUserRolesMapping();
        }
    }
}
