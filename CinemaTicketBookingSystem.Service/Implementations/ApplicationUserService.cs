using CinemaTicketBookingSystem.Data.AppMetaData;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Infrastructure.Context;
using CinemaTicketBookingSystem.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Core.Resources;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailsService;
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IUrlHelper _urlHelper;


        #endregion
        #region Constructors
        public ApplicationUserService(UserManager<ApplicationUser> userManager,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailService emailsService,
                                      ApplicationDBContext applicationDBContext,
                                      IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _applicationDBContext = applicationDBContext;
            _urlHelper = urlHelper;
        }
        #endregion
        #region Handle Functions

        // no forget  unit of work and repository pattern
        public async Task<ApplicationUser> CreateUser(ApplicationUser user, string password)
        {
            var identityResult = await _userManager.CreateAsync(user, password);

            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.FirstOrDefault().Description);

            user = await _userManager.FindByNameAsync(user.UserName);

            if (user == null)
                throw new Exception("user not founded");
            await _userManager.AddToRoleAsync(user, Roles.User);

            await SendConfirmUserEmailToken(user);
            return user;
        }
        public async Task SendConfirmUserEmailToken(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var requestAccessor = _httpContextAccessor.HttpContext.Request;

            var returnURL = requestAccessor.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "ApplicationUser", new { userId = user.Id, code = code });
            var userFullName = user.FullName;
            var message = $"<!DOCTYPE html>\r\n<html>\r\n  <head></head>\r\n  <body style=\"font-family: Arial, sans-serif; line-height: 1.6; color: #333; background-color: #f9f9f9; margin: 0; padding: 0;\">\r\n    <div style=\"max-width: 600px; margin: 20px auto; background: #ffffff; border: 1px solid #dddddd; border-radius: 8px; overflow: hidden;\">\r\n      <div style=\"background: #4caf50; color: #ffffff; text-align: center; padding: 20px;\">\r\n        <h2 style=\"margin: 0;\">Confirm Your Email</h2>\r\n      </div>\r\n      <div style=\"padding: 20px; text-align: left;\">\r\n        <h1 style=\"font-size: 24px; color: #4caf50; margin: 0;\">Hello, {userFullName}!</h1>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\">\r\n          Thank you for registering with us. Please confirm your email address to complete your registration and start using our services.\r\n        </p>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\">Click the button below to confirm your email address:</p>\r\n        <a href='{returnURL}' style=\"display: inline-block; padding: 10px 20px; margin-top: 20px; background: #4caf50; color: #ffffff; text-decoration: none; border-radius: 4px; font-size: 16px;\">Confirm Email</a>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\">\r\n          If the button above doesn't work, you can copy and paste the following link into your browser:\r\n        </p>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\"><a href='{returnURL}' style=\"color: #4caf50; text-decoration: underline;\">[Confirmation Link]</a></p>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\">\r\n          If you didn't create an account with us, please ignore this email.\r\n        </p>\r\n      </div>\r\n      <div style=\"background: #f1f1f1; text-align: center; padding: 10px; font-size: 12px; color: #555;\">\r\n        <p style=\"margin: 0;\">&copy; 2025 Cinema App. All rights reserved.</p>\r\n      </div>\r\n    </div>\r\n  </body>\r\n</html>";
            await _emailsService.SendEmail(user.Email, userFullName, message, "Confirm Email");
        }
        public async Task ConfirmUserEmail(ApplicationUser user, string code)
        {
            if (user.EmailConfirmed == true)
                throw new Exception(NotifiAndAlertsResources.EmailAlreadyConfirmed);

            var identityResult = await _userManager.ConfirmEmailAsync(user, code);

            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.FirstOrDefault().Description);
        }
        public async Task<ApplicationUser?> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<string> SoftDeleteUserAsync(ApplicationUser user, Guid updatedBy)
        {
            user.CurrentState = 0;
            user.UpdatedBy = updatedBy;
            user.UpdatedDateUtc = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return "Succeeded";
            else
                return result.Errors.FirstOrDefault()?.Description ?? "An error occurred while deleting the user";

        }
        public async Task<string> DeleteUserAsync(ApplicationUser user)
        {

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return "Succeeded";
            else
                return result.Errors.FirstOrDefault()?.Description ?? "An error occurred while deleting the user";

        }
        public async Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }
        public async Task<string> UpdateAsync(ApplicationUser user)
        {
            var updatedUser = await _userManager.UpdateAsync(user);
            if (updatedUser.Succeeded)
                return "Succeeded";
            else
                return updatedUser.Errors.FirstOrDefault()?.Description ?? "An error occurred while updating the user"; 
        }
        public async Task<string> ChangePasswordAsync(ApplicationUser user , string oldPassword, string confirmPassword)
        {
            var isPasswordChanged = await _userManager.ChangePasswordAsync(user, oldPassword, confirmPassword);
            if(isPasswordChanged.Succeeded)
                return "Succeeded";
            else
                return isPasswordChanged.Errors.FirstOrDefault()?.Description ?? "An error occurred while changing the password";   

        }
        public async Task<string> SendResetUserPasswordCode(string email)
        {
            await using var transaction = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await FindByEmailAsync(email);
                if (user == null) return "UserNotFound";

                //Generate Random Code & insert it in User Row
                var randomCode = new Random().Next(0, 1000000).ToString("D6");
                user.Code = HashCode(randomCode);
                var identityResult = await _userManager.UpdateAsync(user);
                if (!identityResult.Succeeded)
                {
                    transaction.Rollback();
                    return "ErrorInUpdateUser";
                }

                var userFullName = user.FullName;
               
                var message = $"<!DOCTYPE html>\r\n<html>\r\n  <head></head>\r\n  <body style=\"font-family: Arial, sans-serif; line-height: 1.6; color: #333; background-color: #f9f9f9; margin: 0; padding: 0;\">\r\n    <div style=\"max-width: 600px; margin: 20px auto; background: #ffffff; border: 1px solid #dddddd; border-radius: 8px; overflow: hidden;\">\r\n      <div style=\"background: #4caf50; color: #ffffff; text-align: center; padding: 20px;\">\r\n        <h2 style=\"margin: 0;\">Reset Your Password</h2>\r\n      </div>\r\n      <div style=\"padding: 20px; text-align: left;\">\r\n        <h1 style=\"font-size: 24px; color: #4caf50; margin: 0;\">Hello, {userFullName}!</h1>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\">\r\n          You requested to reset your password. Use the code below to reset it:\r\n        </p>\r\n        <div style=\"margin: 20px 0; text-align: center;\">\r\n          <span style=\"display: inline-block; font-size: 20px; font-weight: bold; color: #4caf50; background: #f1f1f1; padding: 10px 20px; border-radius: 4px; border: 1px solid #dddddd;\">\r\n            {randomCode}\r\n          </span>\r\n        </div>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\">\r\n          Enter this code in the password reset form on our website or app to complete the process.\r\n        </p>\r\n        <p style=\"margin: 10px 0; font-size: 16px;\">\r\n          If you didn’t request this, you can safely ignore this email. Your password will not change unless you use the code above.\r\n        </p>\r\n      </div>\r\n      <div style=\"background: #f1f1f1; text-align: center; padding: 10px; font-size: 12px; color: #555;\">\r\n        <p style=\"margin: 0;\">&copy; 2025 Cinema App. All rights reserved.</p>\r\n      </div>\r\n    </div>\r\n  </body>\r\n</html>\r\n";
                
                await _emailsService.SendEmail(email, userFullName, message, "Reset Cinema App Password");
                await transaction.CommitAsync();
                return "Success";
             
            }
            catch (Exception)
            {
                transaction.Rollback();
                return "Failed";
            }
        }
        public async Task<string> ConfirmResetPasswordCodeAsyn(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return "UserNotFound";

            var hashedSubmittedCode = HashCode(code);

            if (user.Code != hashedSubmittedCode)
                return "InvalidOrExpiredCode"; 

            return "Success";
        }
        public async Task<string> ResetPassword(string ResetCode, string newPassword)
        {
            var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                var hashedSubmittedCode = HashCode(ResetCode);

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Code == hashedSubmittedCode);
                if (user == null)
                    return "Code not founded";

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, newPassword);
                user.Code = null;
                await _userManager.UpdateAsync(user);

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        public IQueryable<ApplicationUser> GetAllUsersQueryable()
        {
            return _userManager.Users.AsQueryable();
        }
        private string HashCode(string code)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(code);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public async Task<ApplicationUser?> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return await _userManager.IsEmailConfirmedAsync(user);
        }


        #endregion
    }
}
