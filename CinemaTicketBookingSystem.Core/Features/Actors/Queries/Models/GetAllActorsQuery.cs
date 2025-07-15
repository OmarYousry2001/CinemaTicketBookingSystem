using MediatR;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;

namespace CinemaTicketBookingSystem.Core.Features.Actors.Queries.Models
{
    public class GetAllActorsQuery : IRequest<Response<List<GetAllActorsResponse>>>
    {
    }
}
