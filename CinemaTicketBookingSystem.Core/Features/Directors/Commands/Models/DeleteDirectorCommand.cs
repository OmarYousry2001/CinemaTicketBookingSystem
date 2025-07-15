using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Directors.Commands.Models
{
    public class DeleteDirectorCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
