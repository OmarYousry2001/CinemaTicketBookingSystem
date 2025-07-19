using AutoMapper;
using CinemaTicketBookingSystem.Core.Features.Users.Commands.Models;
using CinemaTicketBookingSystem.Core.Features.Users.Queries.Results;
using CinemaTicketBookingSystem.Core.GenericResponse;
using CinemaTicketBookingSystem.Data.Entities.Identity;
using CinemaTicketBookingSystem.Data.Resources;
using CinemaTicketBookingSystem.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CinemaTicketBookingSystem.Core.Features.Users.Commands.Handler
{
    public class UserCommandsHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<FindUserByIdResponse>>,
        IRequestHandler<EditUserCommand, Response<FindUserByIdResponse>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangePasswordCommand, Response<string>>,
        IRequestHandler<ConfirmResetPasswordCodeCommand, Response<string>>,
        IRequestHandler<SendResetPasswordCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserService _userService;
        #endregion

        #region Constructors
        public UserCommandsHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IApplicationUserService userService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userService = userService;
        }
        #endregion

        #region Actions
        public async Task<Response<FindUserByIdResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            try
            {
                user = await _userService.CreateUser(user, request.Password);
            }
            catch (Exception ex)
            {
                return BadRequest<FindUserByIdResponse>(ex.Message);
            }

            var userMappedIntoResponse = new FindUserByIdResponse
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };

            return Created(userMappedIntoResponse);
        }
        public async Task<Response<FindUserByIdResponse>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userService.FindByIdAsync(request.Id);
            var user = _mapper.Map(request, oldUser);

            var result = await _userService.UpdateAsync(user);
            if (result != "Succeeded")
                return BadRequest<FindUserByIdResponse>(result);

            var userMappedIntoResponse = new FindUserByIdResponse
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return Success(userMappedIntoResponse);
        }
        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.Id);
            if (user is null)
                return NotFound<string>();

            var result = await _userService.DeleteUserAsync(user);
            if (result != "Succeeded")
                return BadRequest<string>(result);

            return Deleted<string>();
        }
        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.FindByIdAsync(request.Id);
            var PasswordChanged = await _userService.ChangePasswordAsync(user, request.OldPassword, request.ConfirmPassword);
            if (PasswordChanged == "Succeeded") return Success<string>(NotifiAndAlertsResources.Success);
            else return BadRequest<string>(PasswordChanged);

        }
        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.SendResetUserPasswordCode(request.Email);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>(ValidationResources.EmailNotFound);
                case "ErrorInUpdateUser": return BadRequest<string>(ValidationResources.TryAgainInAnotherTime);
                case "Failed": return BadRequest<string>(ValidationResources.TryAgainInAnotherTime);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(ValidationResources.TryAgainInAnotherTime);
            }
        }
        public async Task<Response<string>> Handle(ConfirmResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
                var result = await _userService.ConfirmResetPasswordCodeAsyn(request.Email, request.ResetCode);
                if(result == "UserNotFound") return NotFound<string>(ValidationResources.EmailNotFound);
                if (result == "InvalidOrExpiredCode") return BadRequest<string>(UserResources.InvalidOrExpiredCode);
                return Success(NotifiAndAlertsResources.Successful);
        }
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {

            var result =    await _userService.ResetPassword(request.ResetCode, request.Password);
            if (result == "UserNotFound") return NotFound<string>(ValidationResources.EmailNotFound);
            if (result == "Failed") return BadRequest<string>(ValidationResources.SetNewPasswordFailed);    
            return Success(NotifiAndAlertsResources.Successful);

        }
        #endregion
    }
}
