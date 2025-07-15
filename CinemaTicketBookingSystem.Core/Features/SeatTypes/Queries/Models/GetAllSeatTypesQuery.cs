using CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.SeatTypes.Queries.Models
{
    public class GetAllSeatTypesQuery : IRequest<Response<List<GetAllSeatTypesResponse>>>
    {
    }
}
