using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Halls.Commands.Models
{
    public class EditHallCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string NameAr { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public int Capacity { get; set; }
     
    }
}
