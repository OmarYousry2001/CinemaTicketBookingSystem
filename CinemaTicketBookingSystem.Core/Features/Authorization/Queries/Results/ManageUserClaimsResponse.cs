
namespace CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results
{
    public class ManageUserClaimsResponse
    {
        public string UserId { get; set; }
        public List<UserClaimsInManageUserClaimsResponse> userClaims { get; set; }
    }
    public class UserClaimsInManageUserClaimsResponse
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}
