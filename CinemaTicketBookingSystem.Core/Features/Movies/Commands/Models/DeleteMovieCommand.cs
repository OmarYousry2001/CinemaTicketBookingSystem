using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Movies.Commands.Models
{
    public class DeleteMovieCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
