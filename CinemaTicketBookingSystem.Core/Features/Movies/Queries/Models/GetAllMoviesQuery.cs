using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Movies.Queries.Models
{
    public class GetAllMoviesQuery : IRequest<Response<List<GetAllMoviesResponse>>>
    {

    }
}
