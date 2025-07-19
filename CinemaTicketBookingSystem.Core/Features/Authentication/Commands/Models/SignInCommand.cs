using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Helpers;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthTokenResponse>>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
