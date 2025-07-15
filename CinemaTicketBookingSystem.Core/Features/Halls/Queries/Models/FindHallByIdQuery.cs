using CinemaTicketBookingSystem.Core.Features.Halls.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Halls.Queries.Models
{
    public class FindHallByIdQuery : IRequest<Response<FindHallByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
