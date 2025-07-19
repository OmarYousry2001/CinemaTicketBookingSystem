using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Authentication.Queries.Models
{
    public class GetAccessTokenValidityQuery : IRequest<Response<string>>
    {
        public string AccessToken { get; set; } = default!;
    }
}