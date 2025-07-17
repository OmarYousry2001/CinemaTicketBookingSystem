using CinemaTicketBookingSystem.Data.Entities;
using CinemaTicketBookingSystem.Service.ServiceBase;
using Microsoft.AspNetCore.Http;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface ISeatService : IBaseService<Seat>
    {
        Task<bool> IsExistInHallAsync(string seatNumber, Guid hallId);
        Task<bool> IsExistBySeatIdInHallAsync(Guid seatId, Guid hallId);
        Task<int> CountSeatsInHall(Guid hallId);
        decimal CalculateSeatsPrice(IEnumerable<Seat> seatsList);
        public Task<Seat> FindByIdWithIncludes(Guid id);
    }
}
