

using CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models;
using CinemaTicketBookingSystem.Data.Helpers;
using static CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models.UpdateUserRolesCommand;

namespace CinemaTicketBookingSystem.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public  void  EditUserRoleMapping()
        {
            CreateMap<UpdateUserRolesCommand, UpdateUserRolesRequest>()
                .ForMember(des => des.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(des => des.userRoles, opt => opt.MapFrom(src => src.userRoles));


            CreateMap<RolesInUserRolesResponse, UserRoles>();



        }

    }
}
