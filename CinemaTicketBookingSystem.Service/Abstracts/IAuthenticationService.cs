using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace CinemaTicketBookingSystem.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JwtAuthTokenResponse> GetJwtTokenAsync(ApplicationUser user);
        JwtSecurityToken ReadJwtToken(string accessToken);
        Task<string>? ValidateBeforeRenewTokenAsync(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
        Task<JwtAuthTokenResponse> CreateNewAccessTokenByRefreshToken(string accessToken, UserRefreshToken userRefreshToken);
        Task<string> ValidateAccessTokenAsync(string accessToken);
        Task<UserRefreshToken> GetUserFullRefreshTokenObjByRefreshToken(string refreshToken);
        Task<SignInResult> CheckUserPasswordAsync(ApplicationUser user, string password, bool lockoutOnFailure = false);

    }
}
