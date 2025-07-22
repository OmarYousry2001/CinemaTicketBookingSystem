

using CinemaTicketBookingSystem.Data.Entities.Identity;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface ICurrentUserService
    {
        public Task<ApplicationUser> GetUserAsync();
        public Guid GetUserId();
        public  Task<bool> CheckIfRuleExist(string roleName);
    }
}
