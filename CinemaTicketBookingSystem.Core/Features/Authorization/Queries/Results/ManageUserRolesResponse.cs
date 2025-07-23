
namespace CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results
{
    public class ManageUserRolesResponse
    {
        public string UserId { get; set; }
        public List<UserRolesInManageUserRolesResponse> userRoles { get; set; }
    }
    public class UserRolesInManageUserRolesResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }

}
