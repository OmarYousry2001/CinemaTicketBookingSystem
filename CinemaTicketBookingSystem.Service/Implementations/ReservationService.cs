using CinemaTicketBookingSystem.Data.Entities;
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




        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _reservationRepository.GetTableNoTracking()
         .AnyAsync(d => d.Id == id);
        }
        public async Task<bool> IsSeatExistReservationInSameShowTimeAsync(Guid showTimeId, Guid seatId)
        {
            return await _reservationRepository.GetTableNoTracking()
                .Include(r => r.ShowTime)
                .Include(r => r.ReservationSeats)
                .Where(r => r.ShowTime.Id == showTimeId )
                .AnyAsync(r => r.ReservationSeats.Any(rs => rs.SeatId == seatId));
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

        //public async Task<bool> IsSeatExistReservationInSameShowTimeAsync(Guid showTimeId, Guid seatId)
        //{
        //    return await _reservationRepository.GetTableNoTracking()
        //        .AnyAsync(r => r.ShowTimeId == showTimeId &&
        //                       r.ReservationSeats.Any(rs => rs.SeatId == seatId));
        //}
    }
}
