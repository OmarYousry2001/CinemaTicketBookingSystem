using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Data.Resources;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IAuthorizationService
    {
        Task<Role> AddRoleAsync(string roleName);
        Task<Role> EditRoleAsync(string Id, string roleName);
        Task<bool> IsRoleExistByIdAsync(string Id);
        public  Task<bool> IsRoleExistByName(string roleName);
        Task<IdentityResult> DeleteRoleAsync(string Id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> FindRoleByIdAsync(string id);
        Task<List<string>> GetUserRolesAsync(string userId);
        //omar
        Task<List<string>> UpdateUserRolesAsync(string userId, List<string> rolesNames);

        public Task<ManageUserRolesResult> ManageUserRolesData(ApplicationUser user);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);


    }
}
