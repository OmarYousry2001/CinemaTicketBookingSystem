

using CinemaTicketBookingSystem.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IApplicationUserService
    {
        public Task<ApplicationUser> CreateUser(ApplicationUser user, string password);
        public Task SendConfirmUserEmailToken(ApplicationUser user);
        Task ConfirmUserEmail(ApplicationUser user, string code);
        Task<ApplicationUser?> FindByIdAsync(string userId);
        public Task<string> SoftDeleteUserAsync(ApplicationUser user, Guid updatedBy);
        public Task<string> DeleteUserAsync(ApplicationUser user);
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<string> UpdateAsync(ApplicationUser user);
        public Task<string> ChangePasswordAsync(ApplicationUser user, string oldPassword, string confirmPassword);

        Task<string> SendResetUserPasswordCode(string email);
        Task<string> ConfirmResetPasswordCodeAsyn(string Code, string Email);
        Task<string> ResetPassword(string ResetCode, string newPassword);
        IQueryable<ApplicationUser> GetAllUsersQueryable();
        Task<ApplicationUser?> FindByNameAsync(string userName);
        Task<bool> IsEmailConfirmedAsync(ApplicationUser user);

    }
}
