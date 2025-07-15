using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Halls.Queries.Models
{
    public class GetAllHallsQuery : IRequest<Response<List<GetAllHallsResponse>>>
    {
    }
}
