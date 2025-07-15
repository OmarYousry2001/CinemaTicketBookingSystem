using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Genres.Commands.Models
{
    public class DeleteGenreCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
