
namespace CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results
{
    public class ManageUserClaimsResponse
    {
        public string UserId { get; set; }
        public List<UserClaimsInManageUserClaims> userClaims { get; set; }
    }
    public class UserClaimsInManageUserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
