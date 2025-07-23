using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Helpers;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Infrastructure.InfrastructureBases.UnitOfWork;
using CinemaTicketBookingSystem.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketBookingSystem.Service.Implementations
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Fields
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public AuthorizationService(RoleManager<Role> roleManager
            , UserManager<ApplicationUser> userManager
            , IUnitOfWork  unitOfWork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork =  unitOfWork;  
        }
        #endregion
        #region Functions
        public async Task<Role> AddRoleAsync(string roleName)
        {

            var existingRole = await _roleManager.FindByNameAsync(roleName);
            if (existingRole != null)
                throw new Exception($"The role '{roleName}' already exists.");

            var identityRole = new Role
            {
                Name = roleName,
            };
            var identityResult = await _roleManager.CreateAsync(identityRole);

            if (!identityResult.Succeeded)
                throw new Exception(identityResult.Errors.FirstOrDefault().Description);

            identityRole = await _roleManager.FindByNameAsync(roleName);
            return identityRole;
        }
        public async Task<Role> EditRoleAsync(string Id, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            role.Name = roleName;

            var identityResult = await _roleManager.UpdateAsync(role);
            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.FirstOrDefault().Description);
            }
            return role;
        }
        public async Task<IdentityResult> DeleteRoleAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id) ?? throw new KeyNotFoundException(NotifiAndAlertsResources.NotFound);
            //Check if user has this role or not
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users is not null && users.Count != 0)throw new Exception(ValidationResources.Role_HasAssignedUsers);
            var  result  =  await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
            return result;
        }
        public async Task<bool> IsRoleExistByIdAsync(string Id)
        {
            return await _roleManager.Roles.AnyAsync(r => r.Id == Id);
        }
        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }
        public async Task<Role> FindRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }
        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)  throw new Exception(ValidationResources.UserNotFound);

            var userRolesNames = await _userManager.GetRolesAsync(user);
            return userRolesNames.ToList();
        }

        // omar
        public async Task<List<string>> UpdateUserRolesAsync(string userId, List<string> rolesNames)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new KeyNotFoundException(ValidationResources.UserNotFound);
            var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                var oldUserRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, oldUserRoles);

                var identityResult = await _userManager.AddToRolesAsync(user, rolesNames);

                if (!identityResult.Succeeded)
                    throw new Exception(identityResult.Errors.FirstOrDefault().Description);

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
            return await GetUserRolesAsync(userId);
        }


        public async Task<ManageUserRolesResult> ManageUserRolesData(ApplicationUser user)
        {
            var response = new ManageUserRolesResult();
            var rolesList = new List<UserRoles>();
            //Roles
            var roles = await _roleManager.Roles.ToListAsync();
            response.UserId = user.Id;
            foreach (var role in roles)
            {
                var userRole = new UserRoles();
                userRole.Id = role.Id;
                userRole.Name = role.Name;
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole.HasRole = true;
                }
                else
                {
                    userRole.HasRole = false;
                }
                rolesList.Add(userRole);
            }
            response.userRoles = rolesList;
            return response;
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var transact = await _unitOfWork.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                {
                    throw new Exception(ValidationResources.UserNotFound);
                }
                //get user Old Roles
                var userRoles = await _userManager.GetRolesAsync(user);
                //Delete OldRoles
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                if (!removeResult.Succeeded)
                    throw new Exception(ValidationResources.FailedToRemoveOldRoles);
                var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);

                //Add the Roles HasRole=True
                var addRolesResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addRolesResult.Succeeded)
                    throw new Exception(ValidationResources.FailedToAddNewRoles);
                await transact.CommitAsync();
                //return Result
                return NotifiAndAlertsResources.Success;
            }
            catch (Exception ex)
            {
                await transact.RollbackAsync();
                throw;
            }
        }


        //public async Task<ManageUserClaimsResult> ManageUserClaimData(ApplicationUser user)
        //{
        //    var response = new ManageUserClaimsResult();
        //    var usercliamsList = new List<UserClaims>();
        //    response.UserId = user.Id;
        //    //Get USer Claims
        //    var userClaims = await _userManager.GetClaimsAsync(user); //edit
        //                                                              //create edit get print
        //    foreach (var claim in ClaimsStore.claims)
        //    {
        //        var userclaim = new UserClaims();
        //        userclaim.Type = claim.Type;
        //        if (userClaims.Any(x => x.Type == claim.Type))
        //        {
        //            userclaim.Value = true;
        //        }
        //        else
        //        {
        //            userclaim.Value = false;
        //        }
        //        usercliamsList.Add(userclaim);
        //    }
        //    response.userClaims = usercliamsList;
        //    //return Result
        //    return response;
        //}

        //public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        //{
        //    var transact = await _dbContext.Database.BeginTransactionAsync();
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        //        if (user == null)
        //        {
        //            return "UserIsNull";
        //        }
        //        //remove old Claims
        //        var userClaims = await _userManager.GetClaimsAsync(user);
        //        var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
        //        if (!removeClaimsResult.Succeeded)
        //            return "FailedToRemoveOldClaims";
        //        var claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

        //        var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
        //        if (!addUserClaimResult.Succeeded)
        //            return "FailedToAddNewClaims";

        //        await transact.CommitAsync();
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        await transact.RollbackAsync();
        //        return "FailedToUpdateClaims";
        //    }
        //}
        #endregion
    }
}











//public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
//{
//    var transact = await _unitOfWork.BeginTransactionAsync();
//    try
//    {
//        //Get User
//        var user = await _userManager.FindByIdAsync(request.UserId);
//        if (user == null)
//        {
//            return ValidationResources.UserNotFound;
//        }
//        //get user Old Roles
//        var userRoles = await _userManager.GetRolesAsync(user);
//        //Delete OldRoles
//        var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
//        if (!removeResult.Succeeded)
//            return ValidationResources.FailedToRemoveOldRoles;
//        var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);

//        //Add the Roles HasRole=True
//        var addRolesResult = await _userManager.AddToRolesAsync(user, selectedRoles);
//        if (!addRolesResult.Succeeded)
//            return ValidationResources.FailedToAddNewRoles;
//        await transact.CommitAsync();
//        //return Result
//        return NotifiAndAlertsResources.Success;
//    }
//    catch (Exception ex)
//    {
//        await transact.RollbackAsync();
//        return ValidationResources.FailedToUpdateUserRoles;
//    }
//}
