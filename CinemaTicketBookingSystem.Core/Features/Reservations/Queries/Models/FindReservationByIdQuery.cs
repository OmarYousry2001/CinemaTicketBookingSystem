using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;



namespace CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Models
{
    public class FindReservationByIdQuery : IRequest<Response<FindReservationByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
