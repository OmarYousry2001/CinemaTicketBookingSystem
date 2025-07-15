using CinemaTicketBookingSystem.Core.Features.Genres.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;

namespace CinemaTicketBookingSystem.Core.Features.Genres.Queries.Models
{
    public class GetAllGenresQuery : IRequest<Response<List<GetAllGenresResponse>>>
    {
    }
}
