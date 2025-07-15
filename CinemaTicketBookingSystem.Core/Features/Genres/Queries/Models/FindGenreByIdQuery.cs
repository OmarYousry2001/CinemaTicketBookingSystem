using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Genres.Queries.Models
{
    public class FindGenreByIdQuery : IRequest<Response<FindGenreByIdResponse>>
    {
        public Guid Id { get; set; }
    }
}
