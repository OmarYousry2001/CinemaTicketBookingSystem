using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.ShowTimes.Commands.Models
{
    public class AddShowTimeCommand : IRequest<Response<string>>
    {
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal ShowTimePrice { get; set; }
        public Guid HallId { get; set; }
        public Guid MovieId { get; set; }
    }
}
