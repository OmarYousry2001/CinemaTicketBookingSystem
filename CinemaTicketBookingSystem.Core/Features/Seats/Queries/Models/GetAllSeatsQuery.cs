using CinemaTicketBookingSystem.Core.Features.Seats.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Seats.Queries.Models
{
    public class GetAllSeatsQuery : IRequest<Response<List<GetAllSeatsResponse>>>
    {
    }
}
