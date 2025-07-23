

using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Data.Entities.Identity;

namespace CinemaTicketBookingSystem.Core.Mapping.AuthorizationMapping
{
    public partial class AuthorizationProfile
    {
        public void GetAllRolesMapping()
        {
            CreateMap<Role, GetRolesListResponse>();
        }

    }
}
