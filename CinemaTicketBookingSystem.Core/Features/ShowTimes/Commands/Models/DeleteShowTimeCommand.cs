using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Models
{
    public class DeleteShowTimeCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
