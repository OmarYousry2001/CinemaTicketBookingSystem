using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Data.Enums;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.EntityFrameworkCore;


namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class ReservationService : BaseService<Reservation>, IReservationService
    {
        private readonly ITableRepositoryAsync<Reservation> _reservationRepository;

        public ReservationService(ITableRepositoryAsync<Reservation> seatRepository) : base(seatRepository)
        {
            _reservationRepository = seatRepository;
        }

        public IQueryable<Reservation> GetAllQueryable(DateOnly? Search)
        {
            var queryable = _reservationRepository.GetTableAsTracking()
                .Include(r => r.User)
                .Include(r => r.ShowTime).ThenInclude(st => st.Movie) 
                .Include(r => r.ShowTime).ThenInclude(st => st.Hall)  
                .Include(r => r.ReservationSeats).ThenInclude(rs => rs.Seat)
                .Where(r => r.CurrentState==1)
                .AsSplitQuery();
            if (Search.HasValue)
            {
                // Assuming DateOnly without Time
                var date = Search.Value.ToDateTime(new TimeOnly(0, 0));
                var nextDate = date.AddDays(1);
 
                queryable = queryable.Where(r => r.CreatedDateUtc >= date && r.CreatedDateUtc < nextDate);
            }
            return queryable;
        }
        public  async override Task<Reservation> FindByIdAsync(Guid Id)
        {
            return await _reservationRepository.GetTableAsTracking()
           .Include(r => r.User)
           .Include(r => r.ShowTime).ThenInclude(st => st.Movie) // Include Movie from ShowTime
           .Include(r => r.ShowTime).ThenInclude(st => st.Hall)  // Include Hall from ShowTime
           .Include(r => r.ReservationSeats).ThenInclude(rs => rs.Seat) // Include Seats from ReservationSeats  
           .AsSplitQuery()
           .FirstOrDefaultAsync(r => r.Id == Id && r.CurrentState ==1);
        }
        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _reservationRepository.GetTableNoTracking()
         .AnyAsync(d => d.Id == id);
        }

        /// <summary>
        /// Checks if a specific seat is already reserved for the given show time.
        /// Returns true if the seat exists in any reservation for that show time.
        /// </summary>
        public async Task<bool> IsSeatExistReservationInSameShowTimeAsync(Guid showTimeId, Guid seatId)
        {
            // want return true 
            var s =  await _reservationRepository.GetTableNoTracking()
                    .Include(r => r.ShowTime)
                    .Include(r => r.ReservationSeats)
                    .Where(r => r.ShowTime.Id == showTimeId)
                     .Where(r => r.CurrentState == 1)
                    .AnyAsync(r => r.ReservationSeats.Any(rs => rs.SeatId == seatId));
            return s;
        }

        /// <summary>
        /// Calculates the total price for a reservation.
        /// Adds the base price of the show time multiplied by the number of seats,
        /// and adds extra charges for non-standard seat types.
        /// </summary>
        public decimal CalculateReservationPrice(IEnumerable<Seat> seatsList, decimal showTimePrice)
        {
            // Total price starts with the base show time price for each seat
            decimal totalPrice = showTimePrice * seatsList.Count();

            // Add extra price for premium seats (not standard)
            foreach (var seat in seatsList)
            {
                var type = seat.SeatType.TypeNameEn?.Trim().ToLower();

                if (type != "standard" && type != "عادي")
                {
                    totalPrice += seat.SeatType.SeatTypePrice;
                }
            }

            return totalPrice;
        }

        /// <summary>
        /// Deletes all reservations that have expired and have not been paid.
        /// A reservation is considered expired if the allowed time has passed
        /// and the payment status is not marked as completed.
        /// </summary>
        /// <returns>
        /// The number of deleted reservations.
        /// </returns>
        public async Task<int> DeleteExpiredUnpaidReservationsAsync()
        {
            var notCompletedReservations = await _reservationRepository.GetTableAsTracking()
                .Where(r => r.AllowedTime < DateTime.Now && r.PaymentStatus != PaymentStatusEnum.Completed).ToListAsync();

            await _reservationRepository.DeleteRangeAsync(notCompletedReservations);

            return notCompletedReservations.Count;
        }
        /// <summary>
        /// Retrieves a reservation by its associated payment intent ID.
        /// It loads related data including the user, show time, movie, hall, and reserved seats.
        /// Only active reservations (CurrentState == 1) are considered.
        /// </summary>
        public async Task<Reservation?> GetByPaymentIntentAsync(string paymentIntentId)
        {
            return await _reservationRepository.GetTableAsTracking()
                .Include(r => r.User)
                .Include(r => r.ShowTime).ThenInclude(st => st.Movie) // Include Movie from ShowTime
                .Include(r => r.ShowTime).ThenInclude(st => st.Hall)  // Include Hall from ShowTime
                .Include(r => r.ReservationSeats).ThenInclude(rs => rs.Seat) // Include Seats from ReservationSeats
                .AsSplitQuery()
                .FirstOrDefaultAsync(r => r.PaymentIntentId == paymentIntentId && r.CurrentState==1);
        }

    }
}
