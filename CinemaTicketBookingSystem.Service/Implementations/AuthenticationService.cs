using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.Repositories;
using CinemaTicketBookingSystem.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly ITableRepositoryAsync<UserRefreshToken> _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings, ITableRepositoryAsync<UserRefreshToken> refreshTokenRepository
            , UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
            _signInManager = signInManager; 
        }
        #endregion

        #region Actions

        public async Task<JwtAuthTokenResponse> GetJwtTokenAsync(ApplicationUser user)
        {
            var jwtToken = await GenerateJwtSecurityTokenAsync(user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var savedUserRefreshToken = await SaveUserRefreshTokenAsync(user, accessToken, jwtToken.Id);

            var refreshTokenForResponse = GetRefreshTokenForResponse(savedUserRefreshToken.ExpiryDate, user.UserName, savedUserRefreshToken.RefreshToken);

            return new JwtAuthTokenResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenForResponse
            };
        }
        private async Task<JwtSecurityToken> GenerateJwtSecurityTokenAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await GetClaims(user, userRoles.ToList());

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken = new JwtSecurityToken
            (
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.accessTokenExpireDateInMinutes),
                signingCredentials: signingCred
            );
            return jwtToken;
        }
        private async Task<IEnumerable<Claim>> GetClaims(ApplicationUser user, List<string> userRoles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.UserName),
               new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(ClaimTypes.Email,user.Email),
            };
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return   claims;
        }
        private async Task<UserRefreshToken> SaveUserRefreshTokenAsync(ApplicationUser user, string accessToken, string jwtTokenId)
        {
            var userRefreshToken = new UserRefreshToken()
            {
                User = user,
                CreatedDateUtc = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSettings.refreshTokenExpireDateInDays),
                IsRevoked = false,
                IsUsed = true,
                RefreshToken = GenerateRefreshTokenString(),
                Token = accessToken,
                JwtId = jwtTokenId
            };
            var savedUserRefreshToken = await _refreshTokenRepository.AddAndReturnAsync(userRefreshToken , Guid.Parse(user.Id));

            return savedUserRefreshToken;
        }
        private string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        private RefreshTokenInJwtAuthTokeResponse GetRefreshTokenForResponse(DateTime ExpiryDate, string userName, string tokenString)
        {
            var refreshToken = new RefreshTokenInJwtAuthTokeResponse()
            {
                ExpireAt = ExpiryDate,
                UserName = userName,
                TokenString = tokenString
            };
            return refreshToken;
        }

        public async Task<JwtAuthTokenResponse> CreateNewAccessTokenByRefreshToken(string accessToken, UserRefreshToken userRefreshToken)
        {

            // Generate JWT Security Token
            var userId = ReadJwtToken(accessToken).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var generatedJwtSecurityToken = await GenerateJwtSecurityTokenAsync(user);
            var NewAccessToken = new JwtSecurityTokenHandler().WriteToken(generatedJwtSecurityToken);


            var refreshTokenForResponse = GetRefreshTokenForResponse(userRefreshToken.ExpiryDate, user.UserName, userRefreshToken.RefreshToken);


            userRefreshToken.Token = NewAccessToken;
            await _refreshTokenRepository.UpdateAsync(userRefreshToken , Guid.Parse(userId));

            return new JwtAuthTokenResponse()
            {
                AccessToken = NewAccessToken,
                RefreshToken = refreshTokenForResponse
            };
        }
        public JwtSecurityToken ReadJwtToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
                throw new ArgumentNullException(nameof(accessToken));

            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        public async Task<string>? ValidateBeforeRenewTokenAsync(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            //Validations AccessToken
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                return UserResources.InvalidHashAlgorithm;
            if (jwtToken.ValidTo > DateTime.UtcNow)
                return UserResources.NotExpiredToken;

            //Get User RefreshToken
            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var userRefreshToken = await _refreshTokenRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(x => x.Token == accessToken && x.RefreshToken == refreshToken && x.UserId == userId);

            //Validations User Refresh Token
            if (userRefreshToken == null)
                return UserResources.InvalidRefreshToken;

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userRefreshToken , Guid.Parse(userId));
                return UserResources.ExpiredRefreshToken;
            }
            return null;
        }
        public async Task<string> ValidateAccessTokenAsync(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                    return UserResources.InvalidAccessToken;

                return UserResources.NotExpiredToken;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<UserRefreshToken> GetUserFullRefreshTokenObjByRefreshToken(string refreshToken)
        {
            return await _refreshTokenRepository.GetTableAsTracking()
                .Where(r => r.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();
        }


        public async Task<SignInResult> CheckUserPasswordAsync(ApplicationUser user, string password , bool lockoutOnFailure = false)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        #endregion
    }
}