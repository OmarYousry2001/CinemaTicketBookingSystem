using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface ISeatService : IBaseService<Seat>
    {
        Task<bool> IsExistInHallExcludeItselfAsync(Guid id, string seatNumber, Guid hallId);
        Task<bool> IsExistInHallAsync(string seatNumber, Guid hallId);
        Task<bool> IsExistBySeatIdInHallAsync(Guid seatId, Guid hallId);
        Task<int> CountSeatsInHall(Guid hallId);
        public IQueryable<Seat> GetAllQueryable();
    }
}
