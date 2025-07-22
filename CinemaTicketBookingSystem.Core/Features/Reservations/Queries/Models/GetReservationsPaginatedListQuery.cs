using CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Results;
using MediatR;
using SchoolProject.Core.Wrappers;


namespace CinemaTicketBookingSystem.Core.Features.Reservations.Queries.Models
{
    public class GetReservationsPaginatedListQuery : IRequest<PaginatedResult<GetReservationsPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateOnly? Search { get; set; }
        public GetReservationsPaginatedListQuery()
        {
            PageNumber = 1;
            PageSize = 10;
        }
    }
}
