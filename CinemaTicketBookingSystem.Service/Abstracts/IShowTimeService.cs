
using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Service.ServiceBase;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IShowTimeService : IBaseService<ShowTime>  
    {

        Task<IEnumerable<ShowTime>> GetComingShowTimesAsync();
        Task<bool> IsExistAsync(Guid id);
        Task<bool> IsExistAndInFutureAsync(Guid showTimeId);
        Task<bool> IsExistInSameHallAsync(Guid hallId, DateOnly day, TimeOnly startTime, TimeOnly endTime);
        Task<bool> IsExistInSameHallExcludeItselfAsync(Guid showTimeId, DateOnly day, Guid hallId, TimeOnly startTime, TimeOnly endTime);
    }
}
