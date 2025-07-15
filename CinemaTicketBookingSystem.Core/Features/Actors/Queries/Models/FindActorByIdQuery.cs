using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;


namespace CinemaTicketBookingSystem.Core.Features.Actors.Queries.Models
{
    public class FindActorsByIdQuery : IRequest<Response<FindActorByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
