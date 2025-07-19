using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Users.Commands.Models
{
    public class ChangePasswordCommand : IRequest<Response<string>>
    {
        public string Id { get; set; } = default!;
        public string OldPassword { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
