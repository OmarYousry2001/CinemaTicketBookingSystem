using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : IRequest<Response<FindRoleByIdResponse>>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

