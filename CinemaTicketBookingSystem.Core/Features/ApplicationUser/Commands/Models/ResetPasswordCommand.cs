using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Users.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string ResetCode { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
