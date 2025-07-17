using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.Implementations;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.Abstracts.CMS;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.EntityFrameworkCore;


namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class SeatService : BaseService<Seat>, ISeatService
    {
        private readonly SeatRepository _seatRepository;

        public SeatService(SeatRepository seatRepository) : base(seatRepository)
        {
            _seatRepository = seatRepository;
        }
        public override async Task<IEnumerable<Seat>> GetAllAsync()
        {

            return await _seatRepository.GetTableNoTracking().Where(x => x.CurrentState == 1)
                .Include(x => x.Hall)
                .Include(x => x.SeatType)
                .ToListAsync();
        }
        public async Task<Seat> FindByIdWithIncludes(Guid id)
        {
            return await _seatRepository.FindByIdWithIncludes().FirstOrDefaultAsync(x => x.Id == id);

        }
        public async Task<bool> IsExistInHallAsync(string seatNumber, Guid hallId)
        {
            return await _seatRepository.GetTableNoTracking()
                .AnyAsync(x => x.SeatNumber.ToLower().Trim() == seatNumber.ToLower().Trim() && x.HallId == hallId && x.CurrentState == 1);
        }
        public Task<int> CountSeatsInHall(Guid hallId)
        {
            return _seatRepository.GetTableNoTracking()
                .Where(x => x.HallId == hallId && x.CurrentState == 1)
                .CountAsync();
        }
        public decimal CalculateSeatsPrice(IEnumerable<Seat> seatsList)
        {
            if (seatsList == null || !seatsList.Any())
            {
                return 0;
            }
            decimal totalPrice = 0;
            foreach (var seat in seatsList)
            {
                if (seat.SeatType != null)
                {
                    totalPrice += seat.SeatType.SeatTypePrice;
                }
            }
            return totalPrice;  
        }
        public Task<bool> IsExistBySeatIdInHallAsync(Guid seatId, Guid hallId)
        {
            return _seatRepository.GetTableNoTracking()
                .AnyAsync(x => x.Id == seatId && x.HallId == hallId && x.CurrentState == 1);
        }


    }
}
