using CinemaTicketBookingSystem.Core.Features.Movies.Queries.Results;
using CinemaTicketBookingSystem.Data.Enums;
using MediatR;
using SchoolProject.Core.Wrappers;


namespace CinemaTicketBookingSystem.Core.Features.Movies.Queries.Models
{
    public class GetMoviesPaginatedListQuery : IRequest<PaginatedResult<GetMoviesPaginatedListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search { get; set; }
        public MovieOrderingEnum MovieOrdering { get; set; }
      
    }
}
