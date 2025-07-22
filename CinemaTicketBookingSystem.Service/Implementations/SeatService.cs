using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class SeatService : BaseService<Seat>, ISeatService
    {
        private readonly ITableRepositoryAsync<Seat> _seatRepository;

        public SeatService(ITableRepositoryAsync<Seat> seatRepository) : base(seatRepository)
        {
            _seatRepository = seatRepository;
        }
        public  IQueryable<Seat> GetAllQueryable()
        {
            return _seatRepository.GetTableAsTracking().AsQueryable();
        }
        public override async Task<IEnumerable<Seat>> GetAllAsync()
        {

            return await _seatRepository.GetTableNoTracking().Where(x => x.CurrentState == 1)
                .Include(x => x.Hall)
                .Include(x => x.SeatType)
                .Include(x => x.ReservationSeats).ThenInclude(rs => rs.Reservation)
                .ToListAsync();
        }

        public override async Task<Seat> FindByIdAsync(Guid Id)
        {
            return await _seatRepository.GetTableAsTracking()
                .Include(x => x.Hall)
                .Include(x => x.SeatType)
                .FirstOrDefaultAsync(x => x.Id == Id && x.CurrentState == 1);   
        }

        public async Task<bool> IsExistInHallAsync(string seatNumber, Guid hallId)
        {
            return await _seatRepository.GetTableNoTracking()
                .AnyAsync(x => x.SeatNumber.ToLower().Trim() == seatNumber.ToLower().Trim() && x.HallId == hallId && x.CurrentState == 1);
        }
        public  async Task<int> CountSeatsInHall(Guid hallId)
        {
            return await _seatRepository.GetTableNoTracking()
                .Where(x => x.HallId == hallId && x.CurrentState == 1)
                .CountAsync();
        }
        public async Task<bool> IsExistInHallExcludeItselfAsync(Guid id, string seatNumber, Guid hallId)
        {
            return await _seatRepository.GetTableNoTracking()
                .AnyAsync(d => d.Id != id
                && d.SeatNumber.ToLower().Trim() == seatNumber.ToLower().Trim());

        }
        //public decimal CalculateSeatsPrice(IEnumerable<Seat> seatsList)
        //{
        //    if (seatsList == null || !seatsList.Any())
        //    {
        //        return 0;
        //    }
        //    decimal totalPrice = 0;
        //    foreach (var seat in seatsList)
        //    {
        //        if (seat.SeatType != null)
        //        {
        //            totalPrice += seat.SeatType.SeatTypePrice;
        //        }
        //    }
        //    return totalPrice;  
        //}
        public Task<bool> IsExistBySeatIdInHallAsync(Guid seatId, Guid hallId)
        {
            return _seatRepository.GetTableNoTracking()
                .AnyAsync(x => x.Id == seatId && x.HallId == hallId && x.CurrentState == 1);
        }


    }
}
