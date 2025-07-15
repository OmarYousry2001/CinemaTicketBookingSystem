using MediatR;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Results;

namespace CinemaTicketBookingSystem.Core.Features.Directors.Queries.Models
{
    public class GetAllDirectorsQuery : IRequest<Response<List<GetAllDirectorsResponse>>>
    {
    }
}
