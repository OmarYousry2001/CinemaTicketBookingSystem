using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;
using CinemaTicketBookingSystem.Core.Features.Actors.Queries.Results;
using CinemaTicketBookingSystem.Core.Features.Directors.Queries.Results;


namespace CinemaTicketBookingSystem.Core.Features.Directors.Queries.Models
{
    public class FindDirectorByIdQuery : IRequest<Response<FindDirectorByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
