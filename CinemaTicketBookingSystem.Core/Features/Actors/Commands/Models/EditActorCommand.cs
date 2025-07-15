using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CinemaTicketBookingSystem.Core.Features.Actors.Commands.Models
{
    public class EditActorCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string ImageURL { get; set; } = default!;
        public IFormFile? Image { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
    }
}
