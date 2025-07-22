using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    internal class CurrentUserService : ICurrentUserService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion
        #region Constructors
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Guid GetUserId()
        {
            var claim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier) ??
                throw new UnauthorizedAccessException();
            return Guid.Parse(claim.Value);
            //return claim.Value;
        }

        public async Task<ApplicationUser> GetUserAsync()
        {
            return await _userManager.FindByIdAsync(GetUserId().ToString()) ?? throw new UnauthorizedAccessException();
        }

        public async Task<bool> CheckIfRuleExist(string roleName)
        {
            return await _userManager.IsInRoleAsync(await GetUserAsync(), roleName);
        }

        #endregion
    }
}
