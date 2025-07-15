using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models
{
    public class AddHallCommand : IRequest<Response<string>>
    {
        public string NameAr { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public int Capacity { get; set; }

    }
}
