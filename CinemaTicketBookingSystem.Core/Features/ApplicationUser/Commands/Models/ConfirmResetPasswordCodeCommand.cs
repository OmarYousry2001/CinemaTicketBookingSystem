using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Users.Commands.Models
{
    public class ConfirmResetPasswordCodeCommand : IRequest<Response<string>>
    {
        public string Email { get; set; } = default!;
        public string ResetCode { get; set; } = default!;
    }
}
