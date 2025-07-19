using CinemaTicketBookingSystem.Core.Features.Users.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Users.Queries.Models
{
    public class GetAllUsersQuery : IRequest<Response<List<GetAllUsersResponse>>>
    {
    }
}
