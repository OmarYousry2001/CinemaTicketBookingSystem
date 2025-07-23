

using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Data.Helpers;

namespace CinemaTicketBookingSystem.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public void ManageUserRolesMapping()
        {
            CreateMap<ManageUserRolesResult, ManageUserRolesResponse>()
           .ForMember(des => des.userRoles, opt => opt.MapFrom(src => src.userRoles));

            CreateMap<UserRoles, UserRolesInManageUserRolesResponse>();
            
        }
    }
}
