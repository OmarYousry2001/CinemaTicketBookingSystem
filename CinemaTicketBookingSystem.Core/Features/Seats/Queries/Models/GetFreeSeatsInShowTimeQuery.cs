
using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Seats.Queries.Models
{
    public class GetFreeSeatsInShowTimeQuery : IRequest<Response<List<GetFreeSeatsInShowTimeResponse>>>
    {
        public Guid ShowTimeId { get; set; }

    }
}
