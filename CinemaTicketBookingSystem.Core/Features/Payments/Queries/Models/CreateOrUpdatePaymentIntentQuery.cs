using CinemaTicketBookingSystem.Core.Features.Payments.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using MediatR;


namespace CinemaTicketBookingSystem.Core.Features.Payments.Queries.Models
{
    public class CreateOrUpdatePaymentIntentQuery : IRequest<Response<CreateOrUpdatePaymentIntentResult>>
    {
        public Guid ReservationId { get; set; }
    }
}
