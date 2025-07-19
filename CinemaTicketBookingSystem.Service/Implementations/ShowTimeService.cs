using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class ShowTimeService : BaseService<ShowTime>, IShowTimeService
    {
        #region Fields
        private readonly ITableRepositoryAsync<ShowTime> _showTimeRepository;
        #endregion
        #region Constructors
        public ShowTimeService(ITableRepositoryAsync<ShowTime> showTimeRepository):base(showTimeRepository)
        {
            _showTimeRepository = showTimeRepository;
        }
        #endregion
        #region Methods
        public override async Task<IEnumerable<ShowTime>> GetAllAsync()
        {
            return await _showTimeRepository.GetTableNoTracking()
                .Include(st => st.Movie)
                .Include(st => st.Hall)
                .ToListAsync();
        }
        public override async Task<ShowTime> FindByIdAsync(Guid Id)
        {
            return await _showTimeRepository.GetTableAsTracking()
                    .Include(st => st.Movie)
                .Include(st => st.Hall)
                .FirstOrDefaultAsync(x => x.CurrentState == 1 && x.Id == Id);

        }
        public async Task<bool> IsExistByIdAsync(Guid id)
        {
            return await _showTimeRepository.GetTableNoTracking().AnyAsync(st => st.Id == id);
        }
        public async Task<bool> IsExistInSameHallAsync(Guid hallId, DateOnly day, TimeOnly startTime, TimeOnly endTime)
        {
            return await _showTimeRepository.GetTableNoTracking().Include(st => st.Hall)
                .AnyAsync(st =>
                    st.Hall.Id == hallId &&
                    st.Day == day &&
                    (
                        (startTime >= st.StartTime && startTime < st.EndTime) ||    
                        (endTime > st.StartTime && endTime <= st.EndTime) ||       
                        (startTime <= st.StartTime && endTime >= st.EndTime)       
                    )
                );
        }
        public async Task<bool> IsExistInSameHallExcludeItselfAsync(Guid showTimeId, DateOnly day, Guid hallId, TimeOnly startTime, TimeOnly endTime)
        {
  
            return await _showTimeRepository.GetTableNoTracking().Include(st => st.Hall)
                .AnyAsync(st =>
                    st.Hall.Id == hallId &&
                    st.Day == day &&

                    (
                        (startTime >= st.StartTime && startTime < st.EndTime) ||
                        (endTime > st.StartTime && endTime <= st.EndTime) ||
                        (startTime <= st.StartTime && endTime >= st.EndTime)
                    ) && st.Id != showTimeId
                );
        }
        public async Task<bool> IsExistAsync(Guid id)
        {
            return await _showTimeRepository.GetTableNoTracking().AnyAsync(st => st.Id == id);
        }
        public async Task<bool> IsExistAndInFutureAsync(Guid showTimeId)
        {
            var showTime = await _showTimeRepository.GetTableAsTracking().FirstOrDefaultAsync(st => st.Id == showTimeId);
            return showTime.Day.ToDateTime(showTime.EndTime) > DateTime.Now;
        }
        public async Task<IEnumerable<ShowTime>> GetComingShowTimesAsync()
        {
            var now = DateTime.Now;

            var all = await _showTimeRepository.GetTableAsTracking()
                .Include(st => st.Movie)
                .Include(st => st.Hall)
                .ToListAsync();
            // DayeOnly + TimeOnly to DateTime  
            return all
                .Where(st => st.Day.ToDateTime(st.EndTime) > now); 
        }

        #endregion
    }
}