using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public List<RolesInUserRolesResponse> userRoles { get; set; }
        public class RolesInUserRolesResponse
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public bool HasRole { get; set; }
        }

    }
}
