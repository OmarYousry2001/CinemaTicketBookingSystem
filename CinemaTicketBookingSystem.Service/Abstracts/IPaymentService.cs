
using CinemaTicketBookingSystem.Data.Entities;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IPaymentService
    {
        Task<Reservation?> CreateOrUpdatePaymentIntent(Guid reservationId);
        Task UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded);
    }
}
