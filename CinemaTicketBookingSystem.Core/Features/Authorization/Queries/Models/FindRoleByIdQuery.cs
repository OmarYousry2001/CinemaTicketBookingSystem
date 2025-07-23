using CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;
namespace CinemaTicketBookingSystem.Core.Features.Authorization.Queries.Models
{
    public class FindRoleByIdQuery : IRequest<Response<FindRoleByIdResponse>>
    {
        public string Id { get; set; }
    }
}
