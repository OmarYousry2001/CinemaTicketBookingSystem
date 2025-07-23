using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<FindRoleByIdResponse>>
    {
        public string RoleName { get; set; }
    }
}
