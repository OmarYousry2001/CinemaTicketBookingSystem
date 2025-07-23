using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : IRequest<Response<string>>
    {
            public string UserId { get; set; }
            public List<UserClaimsInManageUserClaimsResponse> userClaims { get; set; }
        public class UserClaimsInManageUserClaimsResponse
        {
            public string Type { get; set; }
            public bool Value { get; set; }
        }
    }
}
