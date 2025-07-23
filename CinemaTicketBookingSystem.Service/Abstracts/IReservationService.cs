using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Service.ServiceBase;
namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IReservationService : IBaseService<Reservation>
    {
        public Task<bool> IsExistAsync(Guid id);
        public Task<bool> IsSeatExistReservationInSameShowTimeAsync(Guid showTimeId, Guid seatId);
        public decimal CalculateReservationPrice(IEnumerable<Seat> seatsList, decimal showTimePrice);
        Task<int> DeleteExpiredUnpaidReservationsAsync();
        public IQueryable<Reservation> GetAllQueryable(DateOnly? Search = null);
        public  Task<Reservation?> GetByPaymentIntentAsync(string paymentIntentId);
    }
}
