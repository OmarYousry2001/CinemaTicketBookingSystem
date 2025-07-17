using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Movies.Queries.Models
{
    public class FindMovieByIdQuery : IRequest<Response<FindMovieByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
