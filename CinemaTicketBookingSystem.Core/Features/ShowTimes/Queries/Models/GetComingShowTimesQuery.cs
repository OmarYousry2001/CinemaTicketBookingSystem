using CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.ShowTimes.Queries.Models
{
    public class GetComingShowTimesQuery : IRequest<Response<List<GetComingShowTimesResponse>>>
    {

    }
}
