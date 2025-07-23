using CinemaTicketBookingSystem.Core.Features.Users.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Users.Queries.Models
{
    public class GetUserReservationsHistoryQuery : IRequest<Response<List<GetUserReservationsHistoryResponse>>>
    {
        public string Id { get; set; } = default!;
    }
}
